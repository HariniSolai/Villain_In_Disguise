using UnityEngine;

public class BayesianNetwork : MonoBehaviour
{
    public static BayesianNetwork instance;

    public bool playerInteractedNPC;
    public bool playerKilledEnemy;
    public bool playerLeftEnemyAlive;
    public bool playerUsedDarkSpell;

    public float playerGoodProbability = 0.5f;

    void Awake()
    {
        instance = this; // singleton reference
    }

    public void CalculateAlignment()
    {
        float score = 0f;

        // Good actions
        if (playerInteractedNPC)
            score += 0.1f;

        if (playerKilledEnemy)
            score += 0.2f;

        // Bad actions
        if (playerLeftEnemyAlive)
            score -= 0.3f;

        if (playerUsedDarkSpell)
            score -= 0.05f;

        playerGoodProbability = Mathf.Clamp01(0.5f + score);

        Debug.Log("Player Good Probability: " + playerGoodProbability);

        DecideNPCReaction();
    }

    void DecideNPCReaction()
    {
        if (playerGoodProbability > 0.7f)
        {
            Debug.Log("NPC trusts player.");
            ScoreManager.instance.trustUpdate(20);
        }
        else if (playerGoodProbability > 0.4f)
        {
            Debug.Log("NPC unsure, low trust.");
            ScoreManager.instance.trustUpdate(10);
        }
        else
        {
            Debug.Log("NPC hostile, lower trust.");
            ScoreManager.instance.trustUpdate(-10);
        }
    }

    public void UsedDarkSpell()
    {
        playerUsedDarkSpell = true;
        CalculateAlignment(); 
    }
}