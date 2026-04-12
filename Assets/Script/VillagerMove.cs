using UnityEngine;
using UnityEngine.AI;

public class VillagerMove : MonoBehaviour
{
    public Transform[] points;
    public Animator animator;

    private NavMeshAgent agent;
    private int currentPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (points.Length > 0)
        {
            agent.SetDestination(points[currentPoint].position);
        }
    }

    void Update()
    {
        if (points.Length == 0 || agent == null) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPoint = (currentPoint + 1) % points.Length;
            agent.SetDestination(points[currentPoint].position);
        }

        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
}