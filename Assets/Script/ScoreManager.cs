using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI potionText;
    public TextMeshProUGUI trustText;
    public TextMeshProUGUI darkInstructText;
    [SerializeField] private Button PotionBtn;
    [SerializeField] private Button DarkSpell;
    [SerializeField] private Button SpeakToNpc;
    [SerializeField] private Button Q1Good;
    [SerializeField] private Button Q1Bad;
    [SerializeField] private Button Q2Fight;
    [SerializeField] private Button Q2NoFight;
    [SerializeField] private GameObject Forcefield;
    [SerializeField] public Slider dragonHP;
    [SerializeField] public Slider turtleHP;
    [SerializeField] public Slider playerHP;
    
    [SerializeField] private Animator dragon; // assign in Inspector
    [SerializeField] private Animator turtle; 
    public AudioClip dragonSound;
    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI gemHint;
    public TextMeshProUGUI soldierHint;
    public TextMeshProUGUI turtleHint;
    public TextMeshProUGUI turtleWonT;

    public TextMeshProUGUI finalHint;
    public TextMeshProUGUI fightHint; 
    private bool failedtrust; 
    private bool earnedTrust = false; 
    private bool playerInVillage = false; 
    public bool NPCInteracted = false; 
    public bool playerInCave = false; 
    public bool dragonDefeated = false; 
    public bool dragonFightStarted = false; 
    public bool turtleFightStarted = false; 
    public bool turtleDefeated = false; 

    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50;
    private int PlayerHealth = 100;
    private int DragonHealth = 100;
    private int turtleHealth = 100;
    private int maxHealth = 100; 
    public AudioClip NPCHeyWhatAreYouDoingSound;
    public AudioClip NPCHeyWhatWasThatSound;
    public AudioClip enemySound;

    void Awake()
    {
        instance = this; // singleton

        if(SpeakToNpc != null)
            SpeakToNpc.gameObject.SetActive(false);
        if(DarkSpell != null)
            DarkSpell.gameObject.SetActive(false);
            
        if(PotionBtn != null)
            PotionBtn.gameObject.SetActive(false);
        if(potionText != null)
            potionText.gameObject.SetActive(false); 
        if(darkInstructText != null)
            darkInstructText.gameObject.SetActive(false); 

        if(turtleHP != null)
            turtleHP.gameObject.SetActive(false);
        if(turtleHint != null)
            turtleHint.gameObject.SetActive(false);

        if(turtleWonT != null)
            turtleWonT.gameObject.SetActive(false); 
        // Initially hide Q1Good
        if (Q1Good != null)
            Q1Good.gameObject.SetActive(false);
        if (Q1Bad != null)
            Q1Bad.gameObject.SetActive(false);
        if (Q2Fight != null)
            Q2Fight.gameObject.SetActive(false);
        if (Q2NoFight != null)
            Q2NoFight.gameObject.SetActive(false);

        if (question1 != null)
            question1.gameObject.SetActive(false);
        if (question2 != null)
            question2.gameObject.SetActive(false);

        if(fightHint != null)
            fightHint.gameObject.SetActive(false); 

        if (!earnedTrust){
            finalHint.gameObject.SetActive(false); 
            dragonHP.gameObject.SetActive(false); 
            playerHP.gameObject.SetActive(false); 
        }
        if (earnedTrust){
            if (turtleDefeated)
            finalHint.gameObject.SetActive(true); 
            soldierHint.gameObject.SetActive(false); 
            gemHint.gameObject.SetActive(false); 
            fightHint.gameObject.SetActive(false);   
        }

        PotionBtn.onClick.AddListener(() =>
        {
            if(gems >= 1){
                potionUpdate(1);
                gemUpdate(-1);
            }
        });

        DarkSpell.onClick.AddListener(() =>
        {
            if(potions > 0)
            {
                potions -= 1;
                if (!playerInCave) {   
                    trustUpdate(-3);
                }
                UpdatePotionDisplay(); 
                BayesianNetwork.instance.UsedDarkSpell();
                reduceDragonhealth(1); 
            }
        });

        Q1Good.onClick.AddListener(() => {
            //play the sound for the NPC talking 
            AudioSource.PlayClipAtPoint(NPCHeyWhatAreYouDoingSound, Camera.main.transform.position);
            NPCInteractionFSM.instance.AnswerGood(); 
            
            //stop showing talk to NPC button
            SpeakToNpc.gameObject.SetActive(false);
            Debug.Log("Q1: calling good function"); 
            if (Q2Fight != null)
                    Q2Fight.gameObject.SetActive(true);
            if (Q2NoFight != null)
                    Q2NoFight.gameObject.SetActive(true); 
            if (question1 != null)
                question1.gameObject.SetActive(false);
            if (question2 != null)
                question2.gameObject.SetActive(true);

        });

        Q1Bad.onClick.AddListener(() => {
            NPCInteractionFSM.instance.AnswerIgnore(); 
            
            //stop showing talk to NPC button
            SpeakToNpc.gameObject.SetActive(false);

            Debug.Log("Q1: calling ignore function"); 
            if (Q2Fight != null)
                    Q2Fight.gameObject.SetActive(true);
            if (Q2NoFight != null)
                    Q2NoFight.gameObject.SetActive(true);
            if (question1 != null)
                question1.gameObject.SetActive(false);
            if (question2 != null)
                question2.gameObject.SetActive(true); 
        });

        Q2Fight.onClick.AddListener(() => {
            //play the NPC's reaction to the enemy 
            AudioSource.PlayClipAtPoint(NPCHeyWhatWasThatSound, Camera.main.transform.position);

            turtleFightStarted = true; 
            
            //play the enemy sound
            AudioSource.PlayClipAtPoint(enemySound, Camera.main.transform.position);
            Debug.Log("Q2: calling Fight function");
            turtleHint.gameObject.SetActive(true); 
            turtleHP.gameObject.SetActive(true);

            playerHP.gameObject.SetActive(true); 

            callCoroutinetoDamagePlayer(); 
            
            //removing the player talk to NPC button
            if (Q2Fight != null)
                Q2Fight.gameObject.SetActive(false);
            if (Q2NoFight != null)
                Q2NoFight.gameObject.SetActive(false);
            if (question2 != null)
                question2.gameObject.SetActive(false);

        });

        Q2NoFight.onClick.AddListener(() => {
            NPCInteractionFSM.instance.AnswerNoFight(); 
            Debug.Log("Q2: calling No Fight function"); 

            potionText.gameObject.SetActive(true); 
            PotionBtn.gameObject.SetActive(true);
            darkInstructText.gameObject.SetActive(true);

            if (Q2Fight != null)
                Q2Fight.gameObject.SetActive(false);
            if (Q2NoFight != null)
                Q2NoFight.gameObject.SetActive(false);
            if (question2 != null)
                question2.gameObject.SetActive(false); 
        });

        SpeakToNpc.onClick.AddListener(() =>
        {
            Debug.Log("Speaking to NPC!");
            NPCInteracted = true; 
            NPCInteractionFSM.instance.StartInteraction();
            
            soldierHint.gameObject.SetActive(false); 

            if (Q1Good != null)
                    Q1Good.gameObject.SetActive(true);
            if (Q1Bad != null)
                    Q1Bad.gameObject.SetActive(true);
            if (question1 != null)
                question1.gameObject.SetActive(true);
       
            //this is where the interaction logic will be
            // it has to go through these questions in order
            // Q1. Speak to the villager? A. Hold a good converstation --> +5 trust, B. Ignore them --> -2 trust
            // Q2. Will you fight? A. Yes --> trigger the release of enemies, B. No 
        });
    }

    void Start()
    {
        playerHP.maxValue = 100;
        playerHP.value = PlayerHealth;
        dragonHP.maxValue = 100;
        dragonHP.value = DragonHealth;
        turtleHP.maxValue = 100;
        turtleHP.value = turtleHealth;

        UpdateGemDisplay();
        UpdatePotionDisplay();
        UpdateNPCTrustDisplay();
        //if (SpeakToNpc != null && playerInVillage && gems >= 10)
            //SpeakToNpc.gameObject.SetActive(true);
    }

    public void gemUpdate(int num)
    {
        gems += num;
        UpdateGemDisplay();
        //if (SpeakToNpc != null && playerInVillage && gems >= 10)
            //SpeakToNpc.gameObject.SetActive(true);
    }

    public void playerInVillageDetected()
    {
        playerInVillage = true; 
        gemUpdate(0); 
    }
    public int gemNumber()
    {
        return gems; 
    }

    public void trustUpdate(int num)
    {
        NPCTrust += num;
        UpdateNPCTrustDisplay();
        if(NPCTrust >= 80)
        {
            earnedTrust = true; 
            finalHint.gameObject.SetActive(true); 
            Forcefield.gameObject.SetActive(false); 
        }
        if (earnedTrust){
            finalHint.gameObject.SetActive(true); 
            soldierHint.gameObject.SetActive(false); 
            gemHint.gameObject.SetActive(false);   
        }
        if(NPCTrust <= 20)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void inCave()
    {
        //remove cave hint 
        finalHint.gameObject.SetActive(false); 
        soldierHint.gameObject.SetActive(false); 
        gemHint.gameObject.SetActive(false);

        //Show fight hint Press f to fight the dragon
        fightHint.gameObject.SetActive(true); 
        dragonHP.gameObject.SetActive(true); 
        playerHP.gameObject.SetActive(true); 

        //set player in cave & dragon fight started
        playerInCave = true; 
        dragonFightStarted = true; 
    }

    public void reducePlayerHealth()
    {
        if(!dragonDefeated){
            //general dragon attack will be from 5-10 hp of damage 
            int damage = UnityEngine.Random.Range(9, 15);
            PlayerHealth -= damage;
            PlayerHealth = Mathf.Clamp(PlayerHealth, 0, maxHealth);
            playerHP.value = PlayerHealth;

            //if the player's health is 0, they die
            if(PlayerHealth <= 5){
                SceneManager.LoadScene(5); 
            }
        }
        
    }
    public void reduceDragonhealth(int t)
    {
        //type of attack:
        //Dark Spell --> 1  (10-13 hp damage)
        //Sword --> 0 (5-10 hp damage)
        if (dragonFightStarted){
            int damage; 
            if(t == 0){
                damage = UnityEngine.Random.Range(5, 10); 
            } else {
                damage = UnityEngine.Random.Range(10, 13); 
            }
        
            DragonHealth -= damage;
            
            DragonHealth = Mathf.Clamp(DragonHealth, 0, maxHealth);
            dragonHP.value = DragonHealth;

            //reducePlayerHealth(); 

            //if the dragons's health is 0, the player wins
            if(DragonHealth <= 10)
            {
                //loading win screen
                // dragon dead anim 
                DarkSpell.gameObject.SetActive(false); 
                dragonDefeated = true; 
                dragonHP.value = 0;

                AudioSource.PlayClipAtPoint(dragonSound, Camera.main.transform.position, 2f);

                dragon.SetTrigger("Fight");
                dragonFightStarted = false; 
                //wait 10 seconds and play the dragon death
                StartCoroutine(LoadSceneAfterDelay());
            }

            if(!dragonDefeated){
                dragon.SetTrigger("Defend");
            }
           
        }
    }
    public void reduceTurtlehealth()
    {
        if (turtleFightStarted){
            int damage = UnityEngine.Random.Range(5, 10); 
            
            turtleHealth -= damage;
            
            turtleHealth = Mathf.Clamp(turtleHealth, 0, maxHealth);
            turtleHP.value = turtleHealth;

            //reducePlayerHealth(); 

            //if the dragons's health is 0, the player wins
            if(turtleHealth <= 10)
            {
                //loading win text and reset player's health
                turtleHint.gameObject.SetActive(false); 
                turtleWonT.gameObject.SetActive(true);
                PlayerHealth = 100;
                playerHP.value = PlayerHealth; 
                
                //send off data that the player engaged and won the fight
                NPCInteractionFSM.instance.AnswerFight(); 

                // turtle dead anim
                turtleDefeated = true; 
                turtleHP.value = 0;

                AudioSource.PlayClipAtPoint(enemySound, Camera.main.transform.position, 2f);

                turtle.SetTrigger("Death");
                turtleFightStarted = false; 

                //display string that YOU WON 

                //Wait 5 seconds then change to cave instructions
                
                StartCoroutine(nextSteps());
            }

            if(!turtleDefeated){
                turtle.SetTrigger("tDef");
                Debug.Log("turtledefend"); 
            }
           
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(4);
    }

    private IEnumerator nextSteps()
    {
        yield return new WaitForSeconds(5f);
        turtleWonT.gameObject.SetActive(false);
        turtleHint.gameObject.SetActive(false); 
        turtleHP.gameObject.SetActive(false); 
        finalHint.gameObject.SetActive(true); 
        potionText.gameObject.SetActive(true); 
        PotionBtn.gameObject.SetActive(true); 
        DarkSpell.gameObject.SetActive(true); 
        darkInstructText.gameObject.SetActive(true); 

    }

    private void callCoroutinetoDamagePlayer()
    {
        StartCoroutine(damagePlayerByTurtle());
    }
    private IEnumerator damagePlayerByTurtle()
    {
        yield return new WaitForSeconds(4f);
        ScoreManager.instance.reducePlayerHealth(); 
        if(!turtleDefeated)
            callCoroutinetoDamagePlayer(); 
    }

    public void potionUpdate(int num)
    {
        potions += num;
        UpdatePotionDisplay();
    }

    private void UpdateNPCTrustDisplay()
    {
        if (trustText != null)
        {
            trustText.text = "Trust : " + NPCTrust;
        }
    }

    private void UpdateGemDisplay()
    {
        if (gemText != null)
        {
            gemText.text = "Gems: " + gems;
        }
        if(gems < 10 && NPCTrust < 80) {
            gemHint.gameObject.SetActive(true); 
            soldierHint.gameObject.SetActive(false); 
            SpeakToNpc.gameObject.SetActive(false); 
        } else if (gems >= 10 && !NPCInteracted) {
            gemHint.gameObject.SetActive(false);   
            soldierHint.gameObject.SetActive(true); 
        }
    }

    private void UpdatePotionDisplay()
    {
        if (potionText != null)
        {
            potionText.text = "Potions: " + potions;
        }
    }
}