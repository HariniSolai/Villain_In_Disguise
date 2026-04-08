using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AreaSound : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the area
        if (other.CompareTag("Player"))
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Optional: Stop sound when player leaves
        if (other.CompareTag("Player"))
        {
            _audioSource.Stop();
        }
    }
}
