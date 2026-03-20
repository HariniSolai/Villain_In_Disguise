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
    private int gems = 0;
    private int potions = 0;
    private int NPCTrust = 50; 


    void Awake()
    {
        instance = this; // singleton

        // add a listener to the host button
        PotionBtn.onClick.AddListener(() =>
        {
            AddPotion(); 
            RemoveGem(); 
            UpdateGemDisplay(); 
            UpdatePotionDisplay(); 
        });
        
        DarkSpell.onClick.AddListener(() =>
        {
            reduceTrust(); 
            UpdateNPCTrustDisplay(); 
        });
    }

    void Start()
    {
        UpdateGemDisplay();
        UpdatePotionDisplay(); 
        UpdateNPCTrustDisplay(); 
    }

    public void AddGem()
    {
        gems++;
        UpdateGemDisplay();
    }
    public void RemoveGem()
    {
        gems--;
        UpdateGemDisplay();
    }

    public void reduceTrust()
    {
        NPCTrust-=5;
        UpdateNPCTrustDisplay();
    }


    public void AddPotion()
    {
        potions++; 
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