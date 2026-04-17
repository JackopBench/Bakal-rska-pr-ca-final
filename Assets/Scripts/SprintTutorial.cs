using UnityEngine;
using System.Collections;

public class SprintTutorial : MonoBehaviour
{
    public CanvasGroup sprintText;
    public float fadeDuration = 1.5f;
    public float showTime = 2f;

    [Header("UI")]
    public GameObject healthBar;
    public GameObject staminaBar;
    public GameObject staminaFrame;

    private bool playerInZone = false;
    private bool tutorialShown = false;

    void Start()
    {
        sprintText.alpha = 0;

        if (healthBar != null)
            healthBar.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(false);

        if (staminaFrame != null)
            staminaFrame.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && !tutorialShown && Input.GetKeyDown(KeyCode.LeftShift))
        {
            tutorialShown = true;
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        float time = 0;

        // Fade in
        while (time < fadeDuration)
        {
            sprintText.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        sprintText.alpha = 1;

        yield return new WaitForSeconds(showTime);

        // Fade out
        time = 0;

        while (time < fadeDuration)
        {
            sprintText.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        sprintText.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;

            if (staminaBar != null)
                staminaBar.SetActive(true);

            if (staminaFrame != null)
                staminaFrame.SetActive(true);

            if (healthBar != null)
                healthBar.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}