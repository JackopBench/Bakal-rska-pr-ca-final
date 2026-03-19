using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hpBars;
    public int health = 3;
    private DDAManager ddaManager;

    [Header("Game Over")]
    public Image gameOverPanel;
    public float fadeSpeed = 2f;
    public GameObject gameOverUI;

    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
    }

    public void TakeDamage()
    {
        health--;

        if (health >= 0 && health < hpBars.Length)
        {
            hpBars[health].enabled = false;
        }

        if (ddaManager != null)
        {
            ddaManager.OnDamageTaken();
        }

        // 👉 GAME OVER CHECK
        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public bool Heal()
    {
        if (health >= hpBars.Length)
            return false;

        hpBars[health].enabled = true;
        health++;

        return true;
    }

    IEnumerator GameOver()
{
    float alpha = 0f;

    
    if (MusicManager.instance != null)
    {
        MusicManager.instance.StopAllMusicSmooth();
    }

   
    while (alpha < 1f)
    {
        alpha += Time.deltaTime * fadeSpeed;

        Color c = gameOverPanel.color;
        c.a = alpha;
        gameOverPanel.color = c;

        yield return null;
    }

    
    yield return new WaitForSecondsRealtime(1.5f);

    
    gameOverUI.SetActive(true);

    
    Time.timeScale = 0f;
}
}