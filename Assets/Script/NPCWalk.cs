using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public float speed = 2f;
    public Animator animator;

    void Update()
    {
        // Move forward constantly
        Vector3 forward = transform.forward * speed * Time.deltaTime;
        
        // Apply movement
        CharacterController cc = GetComponent<CharacterController>();
        if(cc != null)
        {
            cc.Move(forward);
        }
        else
        {
            // fallback: move via transform
            transform.position += forward;
        }

        // Play walk/run animation
        if(animator != null)
        {
            animator.SetBool("isWalking", true); // make sure your Animator has a bool
        }
    }
}