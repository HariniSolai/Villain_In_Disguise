using UnityEngine;

public class DragonFightTrigger : MonoBehaviour
{
    [SerializeField] private Animator animFight; // assign in Inspector
    public AudioClip dragonSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision of player to cave detected!");

            if (animFight != null)
            {
                animFight.SetTrigger("Fight");
                AudioSource.PlayClipAtPoint(dragonSound, transform.position);
                Debug.Log("Fight Started!");
            }
            else
            {
                Debug.LogError("Animator not assigned!");
            }

            Destroy(gameObject); // destroy trigger only
        }
    }
}