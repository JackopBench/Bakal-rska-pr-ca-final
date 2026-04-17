using UnityEngine;
using System.Collections;

public class TutorialWelcome : MonoBehaviour
{
    public CanvasGroup welcomeText;
    public CanvasGroup wasdText;

    public float fadeDuration = 1.5f;
    public float showTime = 2f;

    void Start()
    {
        welcomeText.alpha = 0;
        wasdText.alpha = 0;

        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        yield return FadeIn(welcomeText);
        yield return new WaitForSeconds(showTime);
        yield return FadeOut(welcomeText);

        yield return FadeIn(wasdText);
        yield return new WaitForSeconds(showTime);
        yield return FadeOut(wasdText);
    }

    IEnumerator FadeIn(CanvasGroup canvas)
    {
        float time = 0;

        while (time < fadeDuration)
        {
            canvas.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = 1;
    }

    IEnumerator FadeOut(CanvasGroup canvas)
    {
        float time = 0;

        while (time < fadeDuration)
        {
            canvas.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = 0;
    }
}