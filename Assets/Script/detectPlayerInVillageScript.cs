using UnityEngine;

public class detectPlayerInVillageScript : MonoBehaviour
{
    public GameObject speakToNPCButton; // assign your button here

    private void Start()
    {
        speakToNPCButton.SetActive(false); // make sure it's hidden initially
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.playerInVillageDetected(); 
            if(ScoreManager.instance.gemNumber() >= 10)
            {
                speakToNPCButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speakToNPCButton.SetActive(false);
        }
    }
}
