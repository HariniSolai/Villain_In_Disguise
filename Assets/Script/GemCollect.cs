using UnityEngine;

public class GemCollect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.gemUpdate(10);

            Destroy(gameObject);
        }
    }
}