using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class DragonFightTrigger : MonoBehaviour
{
    [SerializeField] private Animator animFight; // assign in Inspector
    [SerializeField] private GameObject wall; // assign in Inspector

    public AudioClip dragonSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision of player to cave detected!");

            if (animFight != null)
            {
                //The player is in the cave 
                ScoreManager.instance.inCave();
                //trigger roar
                AudioSource.PlayClipAtPoint(dragonSound, Camera.main.transform.position, 2f);
                animFight.SetTrigger("Awake");
                Debug.Log("Fight Started!");
                callCoroutine(); 
            }
            else
            {
                Debug.LogError("Animator not assigned!");
            }

        }
    }
    private void callCoroutine()
    {
        StartCoroutine(damagePlayer());
    }
    private IEnumerator damagePlayer()
    {
        yield return new WaitForSeconds(3f);
        ScoreManager.instance.reducePlayerHealth(); 
        callCoroutine(); 
        
        
    }
}