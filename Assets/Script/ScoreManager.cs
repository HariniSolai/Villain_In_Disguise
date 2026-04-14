using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal.Internal;

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

    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI finalHint;
    private bool failedtrust; 
    private bool earnedTrust = false; 
    private bool playerInVillage = false; 

    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50;
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

        if (!earnedTrust){
            finalHint.gameObject.SetActive(false); 
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
            trustUpdate(-5);
            BayesianNetwork.instance.UsedDarkSpell();
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
            NPCInteractionFSM.instance.StartInteraction();
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
    }

    private void UpdatePotionDisplay()
    {
        if (potionText != null)
        {
            potionText.text = "Potions: " + potions;
        }
    }
}