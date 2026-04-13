using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
                animFight.SetTrigger("Fight");
                AudioSource.PlayClipAtPoint(dragonSound, Camera.main.transform.position);
                Debug.Log("Fight Started!");
                
                //after 10 seconds move to endScreen
                StartCoroutine(LoadSceneAfterDelay());
            }
            else
            {
                Debug.LogError("Animator not assigned!");
            }

        }
    }
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(4);
        
    }
}