using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeSpeed = 1.5f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        fadeGroup.alpha = 1;

        while (fadeGroup.alpha > 0)
        {
            fadeGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeGroup.alpha = 0;
    }

    IEnumerator FadeOut(string sceneName)
    {
        while (fadeGroup.alpha < 1)
        {
            fadeGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}