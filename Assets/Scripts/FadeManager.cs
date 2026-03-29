using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public CanvasGroup interactText;
    public float fadeSpeed = 1f;
    public GameObject gameOverPanel;

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;

            // Fade obrazovky
            fadeImage.color = new Color(0, 0, 0, alpha);

            // Fade textu
            if (interactText != null)
                interactText.alpha = 1f - alpha;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void FadeToGameOver()
    {   
        StartCoroutine(FadeOutGameOver());
    }

    IEnumerator FadeOutGameOver()
    {
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        gameOverPanel.SetActive(true);
    }
}