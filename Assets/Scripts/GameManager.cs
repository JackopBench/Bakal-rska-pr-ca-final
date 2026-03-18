using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    private bool hasWon = false;
    public GameObject winPanel;

    public void WinGame()
    {
        if (hasWon) return;

        hasWon = true;
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);

        yield return new WaitForSeconds(2f);

        winPanel.SetActive(true);
    }
}