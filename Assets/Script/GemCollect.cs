using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public AudioClip pickupSound;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.gemUpdate(1);

            // Play sound at the item's current position
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}