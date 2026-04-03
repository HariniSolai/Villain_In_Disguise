using UnityEngine;
using UnityEngine.AI;

public class CowWander : MonoBehaviour
{
    public float wanderRadius = 6f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        PickNewDestination();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;

            if (animator != null)
                animator.SetFloat("speed", 0f);

            if (waitTimer >= waitTime)
            {
                PickNewDestination();
                waitTimer = 0f;
            }
        }
        else
        {
            if (animator != null)
                animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    void PickNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}