using UnityEngine;
using System.Collections;

public class DoorInteraction : MonoBehaviour
{
    public SpriteRenderer doorRenderer;
    public Color highlightColor = Color.green;
    private Color originalColor;

    public GameObject pressEText;

    [Header("Teleport")]
    public Transform player;
    public Transform teleportPoint;

    [Header("Tutorial After Teleport")]
    public CanvasGroup tutorialText;
    public float fadeDuration = 1.5f;
    public float showTime = 2f;

    private bool playerInRange;

    void Start()
    {
        originalColor = doorRenderer.color;
        pressEText.SetActive(false);

        if (tutorialText != null)
            tutorialText.alpha = 0;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            player.position = teleportPoint.position;

            if (tutorialText != null)
                StartCoroutine(ShowTutorial());
        }
    }

    IEnumerator ShowTutorial()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            tutorialText.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        tutorialText.alpha = 1;

        yield return new WaitForSeconds(showTime);

        time = 0;

        while (time < fadeDuration)
        {
            tutorialText.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        tutorialText.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            doorRenderer.color = highlightColor;
            pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            doorRenderer.color = originalColor;
            pressEText.SetActive(false);
        }
    }
}