using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;

    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50;

    void Awake()
    {
        instance = this; // singleton

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
            NPCInteractionFSM.instance.AnswerGood(); 
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
            Debug.Log("Q2: calling Fight function"); 

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

        if (SpeakToNpc != null)
            SpeakToNpc.gameObject.SetActive(gems >= 10);
    }

    public void gemUpdate(int num)
    {
        gems += num;
        UpdateGemDisplay();
        if (SpeakToNpc != null)
            SpeakToNpc.gameObject.SetActive(gems >= 10);
    }

    public void trustUpdate(int num)
    {
        NPCTrust += num;
        UpdateNPCTrustDisplay();
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