using UnityEngine;

public class NPCInteractionFSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Question1,
        Question2,
        Combat,
        End
    }
    public GameObject q1UI;
    public GameObject q2UI;

    public State currentState = State.Idle;

    public static NPCInteractionFSM instance;

    void Awake()
    {
        instance = this;
    }
    [SerializeField] private Animator animFight; 
    public AudioClip enemySound;

    void Idle()
    {
        q1UI.SetActive(false);
        q2UI.SetActive(false);
    }

    public void StartInteraction()
    {
        Debug.Log("Starting NPC Interaction...");
        currentState = State.Question1;
        q1UI.SetActive(true);
    }

    // Q1: Talk or Ignore
    public void AnswerGood()
    {
        q1UI.SetActive(false);
        q2UI.SetActive(true);

        Debug.Log("Player chose good conversation");
        ScoreManager.instance.trustUpdate(5);
        BayesianNetwork.instance.playerInteractedNPC = true;

        currentState = State.Question2;
    }

    public void AnswerIgnore()
    {
        q1UI.SetActive(false);
        q2UI.SetActive(true);

        Debug.Log("Player ignored NPC");
        ScoreManager.instance.trustUpdate(-2);
        currentState = State.Question2;
                
    }

    // Q2: Fight or Not
    public void AnswerFight()
    {
        //if (currentState != State.Question2) return;
        Debug.Log("Player chose to fight");
        currentState = State.Combat;

        // Spawn enemy + start trust timer
        EnemySpawner.instance.SpawnEnemy();
        Debug.Log("fight sound played"); 
        AudioSource.PlayClipAtPoint(enemySound, transform.position);

        animFight.SetTrigger("FightPlayer"); 
        BayesianNetwork.instance.playerKilledEnemy = true; 
        BayesianNetwork.instance.playerLeftEnemyAlive = false; 
        BayesianNetwork.instance.StartEnemyTrustTimer();
    }

    public void AnswerNoFight()
    {
        //if (currentState != State.Question2) return;
        Debug.Log("Player avoided fight");
        currentState = State.End;
        BayesianNetwork.instance.playerLeftEnemyAlive = true; 
        BayesianNetwork.instance.CalculateAlignment();
    }
}