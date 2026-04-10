using UnityEngine;
using System.Collections;

public class AreaSoundVillage : MonoBehaviour
{
    public AudioSource villageAudio;
    public float fadeDuration = 2f; // time to fade

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        villageAudio.volume = 0;
        villageAudio.Play();

        float time = 0;
        while (time < fadeDuration)
        {
            villageAudio.volume = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        villageAudio.volume = 1;
    }

    IEnumerator FadeOut()
    {
        float startVolume = villageAudio.volume;
        float time = 0;

        while (time < fadeDuration)
        {
            villageAudio.volume = Mathf.Lerp(startVolume, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        villageAudio.volume = 0;
        villageAudio.Stop();
    }
}