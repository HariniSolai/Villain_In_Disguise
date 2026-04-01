using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public float speed = 2f;
    public float walkDistance = 30f;
    public Animator animator;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;

        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.Move(move);
        }
        else
        {
            transform.position += move;
        }

        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance >= walkDistance)
        {
            // Turn around
            transform.Rotate(0f, 180f, 0f);

            // Reset distance check
            startPosition = transform.position;
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }
}