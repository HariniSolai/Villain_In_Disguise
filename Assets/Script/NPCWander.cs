using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{
    public float walkRadius = 10f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private float waitTimer;
    private bool waiting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GoToNewPoint();
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!waiting)
            {
                waiting = true;
                waitTimer = waitTime;
                agent.ResetPath();
            }
            else
            {
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    waiting = false;
                    GoToNewPoint();
                }
            }
        }
    }

    void GoToNewPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}