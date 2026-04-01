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

    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50;

    void Awake()
    {
        instance = this; // singleton

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

        SpeakToNpc.onClick.AddListener(() =>
        {
            Debug.Log("Speaking to NPC!");
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