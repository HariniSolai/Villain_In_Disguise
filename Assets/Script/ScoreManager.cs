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
    [SerializeField] private Button PotionBtn;
    [SerializeField] private Button DarkSpell;
    [SerializeField] private Button SpeakToNpc;
    [SerializeField] private Button Q1Good;
    [SerializeField] private Button Q1Bad;
    [SerializeField] private Button Q2Fight;
    [SerializeField] private Button Q2NoFight;
    [SerializeField] private GameObject Forcefield;
    [SerializeField] public Slider dragonHP;
    [SerializeField] public Slider playerHP;
    
    [SerializeField] private Animator dragon; // assign in Inspector
    public AudioClip dragonSound;

    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI gemHint;
    public TextMeshProUGUI soldierHint;
    public TextMeshProUGUI finalHint;
    public TextMeshProUGUI fightHint; 
    private bool failedtrust; 
    private bool earnedTrust = false; 
    private bool playerInVillage = false; 
    public bool NPCInteracted = false; 
    public bool playerInCave = false; 
    public bool dragonDefeated = false; 

    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50;
    private int PlayerHealth = 100;
    private int DragonHealth = 100;
    private int maxHealth = 100; 
    public AudioClip NPCHeyWhatAreYouDoingSound;
    public AudioClip NPCHeyWhatWasThatSound;
    public AudioClip enemySound;

    void Awake()
    {
        instance = this; // singleton

        if(SpeakToNpc != null)
            SpeakToNpc.gameObject.SetActive(false);

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
            if(!playerInCave && potions > 0)
            {
                potions -= 1; 
                UpdatePotionDisplay(); 
                trustUpdate(-5);
                BayesianNetwork.instance.UsedDarkSpell();
            }
            if (playerInCave && potions > 0)
            {
                reduceDragonhealth(1); 
                potions -= 1; 
                UpdatePotionDisplay(); 
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
            NPCInteractionFSM.instance.AnswerFight(); 

            //play the NPC's reaction to the enemy 
            AudioSource.PlayClipAtPoint(NPCHeyWhatWasThatSound, Camera.main.transform.position);
            
            //play the enemy sound
            AudioSource.PlayClipAtPoint(enemySound, Camera.main.transform.position);
            Debug.Log("Q2: calling Fight function"); 
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
        dragonHP.value = PlayerHealth;

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

        //set player in cave
        playerInCave = true; 
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
            //dragon.ResetTrigger("Defend");
            dragon.SetTrigger("Fight");
            //wait 10 seconds and play the dragon death
            StartCoroutine(LoadSceneAfterDelay());
        }

        if(!dragonDefeated){
            dragon.SetTrigger("Defend");
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(4);
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