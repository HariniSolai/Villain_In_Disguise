using UnityEngine;
using System.Collections;

public class AreaSoundZone : MonoBehaviour
{
    public AudioSource areaAudio;
    public float fadeDuration = 2f;

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
        areaAudio.volume = 0f;
        areaAudio.Play();

        float time = 0f;
        while (time < fadeDuration)
        {
            areaAudio.volume = Mathf.Lerp(0f, 1f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        areaAudio.volume = 1f;
    }

    IEnumerator FadeOut()
    {
        float startVolume = areaAudio.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            areaAudio.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        areaAudio.volume = 0f;
        areaAudio.Stop();
    }
}