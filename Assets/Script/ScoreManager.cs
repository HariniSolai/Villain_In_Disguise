using UnityEngine;
using TMPro;

using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI gemText;
    public TextMeshProUGUI potionText;
    [SerializeField] private Button PotionBtn;
    private int gems = 0;
    private int potions = 0;

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
    }

    void Start()
    {
        UpdateGemDisplay();
        UpdatePotionDisplay(); 
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


    public void AddPotion()
    {
        potions++; 
        UpdatePotionDisplay(); 
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