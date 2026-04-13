using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class detectPlayerInVillageScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision of player to village detected!");

            ScoreManager.instance.playerInVillageDetected(); 

            Destroy(gameObject); 
        }
    }
}
