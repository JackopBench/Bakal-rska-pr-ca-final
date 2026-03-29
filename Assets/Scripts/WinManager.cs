using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public Image fadeImage;
    public float fadeSpeed = 1f;

    public void WinGame()
    {
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}