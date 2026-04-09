using UnityEngine;
using System.Collections;

public class BayesianNetwork : MonoBehaviour
{
    public static BayesianNetwork instance;

    public bool playerInteractedNPC;
    public bool playerKilledEnemy;
    public bool playerLeftEnemyAlive;
    public bool playerUsedDarkSpell;
    public bool bayesianCalculated = false; 

    public float playerGoodProbability = 0.5f;
    public int changeInTrust = 0; 

    [Header("Enemy Settings")]
    public GameObject enemy; // assign the enemy from the scene
    public float trustPenaltyDelay = 10f; // seconds before reducing trust if enemy alive
    public float NPCTrust = 10f; // seconds before reducing trust if enemy alive

    void Awake()
    {
        instance = this; // singleton reference
    }

    // Call this when you want to calculate trust immediately
    public void CalculateAlignment()
    {
        float score = 0f;

        // Good actions
        if (playerInteractedNPC){
            score += 0.1f;
            Debug.Log("1. player interacted with NPC"); 
        }

        if (playerKilledEnemy){
            score += 0.2f;
            Debug.Log("2. player killed enemy "); 
        }
        // Bad actions
        if (playerLeftEnemyAlive){
            score -= 0.3f;
            Debug.Log("3. player left enemy alive"); 
        }
        if (playerUsedDarkSpell){
            score -= 0.05f;
            Debug.Log("4. player used dark spell"); 
        }
        playerGoodProbability = Mathf.Clamp01(0.5f + score);

        Debug.Log("Player Good Probability: " + playerGoodProbability);

        DecideNPCReaction();
    }

    void DecideNPCReaction()
    {
        if (playerGoodProbability > 0.7f)
        {
            Debug.Log("NPC trusts player.");
            ScoreManager.instance.trustUpdate(22);
            changeInTrust = 10; 
        }
        else if (playerGoodProbability > 0.4f)
        {
            Debug.Log("NPC unsure, low trust.");
            ScoreManager.instance.trustUpdate(11);
            changeInTrust = 3; 

        }
        else
        {
            Debug.Log("NPC hostile, lower trust.");
            ScoreManager.instance.trustUpdate(-8);
            changeInTrust = -10; 
        }
        Debug.Log("NPC trust change!");
        ScoreManager.instance.trustUpdate(changeInTrust);

    }

    // Just update state; don't calculate immediately
    public void UsedDarkSpell()
    {
        playerUsedDarkSpell = true;
    }

    // Call this when the spawn button is clicked
    public void StartEnemyTrustTimer()
    {
        if (enemy != null)
            playerKilledEnemy = true; 
            playerLeftEnemyAlive = false; 
            StartCoroutine(EnemyAliveTimer());
    }

    // Coroutine to reduce trust if enemy still alive after delay
    private IEnumerator EnemyAliveTimer()
    {
        yield return new WaitForSeconds(trustPenaltyDelay);

        if(bayesianCalculated == false)
        {
            bayesianCalculated = true; 
            CalculateAlignment(); 
        }
    }
}