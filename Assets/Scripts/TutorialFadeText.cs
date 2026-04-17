using UnityEngine;
using System.Collections;

public class TutorialFadeText : MonoBehaviour
{
    public CanvasGroup textCanvas;
    public float fadeDuration = 1.5f;
    public float showTime = 2f;

    private bool triggered = false;

    void Start()
    {
        textCanvas.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FadeText());
        }
    }

    IEnumerator FadeText()
    {
        float time = 0;

        // Fade in
        while (time < fadeDuration)
        {
            textCanvas.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        textCanvas.alpha = 1;

        yield return new WaitForSeconds(showTime);

        // Fade out
        time = 0;

        while (time < fadeDuration)
        {
            textCanvas.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        textCanvas.alpha = 0;
    }
}