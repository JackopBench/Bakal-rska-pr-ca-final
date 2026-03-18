using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public GameObject interactText;

    private bool playerInRange = false;

    private SpriteRenderer sr;

    public Color defaultColor = Color.white;
    public Color highlightColor = Color.green;

    public GameManager gameManager;
    public KeyCounter keyCounter;

    void Start()
    {
        // nájde SpriteRenderer (aj ak je na child objekte)
        sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
            sr.color = defaultColor;
        else
            Debug.LogError("SpriteRenderer not found on Door!");

        // ✅ FIX: správny názov premennej (malé i)
        if (interactText == null)
            interactText = GameObject.Find("InteractText");

        if (interactText != null)
            interactText.SetActive(false);
        else
            Debug.LogError("InteractText not found in scene!");

        // nájde GameManager
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
            Debug.LogError("GameManager not found!");

        // nájde KeyCounter
        if (keyCounter == null)
            keyCounter = FindFirstObjectByType<KeyCounter>();

        if (keyCounter == null)
            Debug.LogError("KeyCounter not found!");
    }   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("hitBox")) return;

        playerInRange = true;

        if (interactText != null)
            interactText.SetActive(true);

        sr.color = highlightColor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("hitBox")) return;

        playerInRange = false;

        if (interactText != null)
            interactText.SetActive(false);

        sr.color = defaultColor;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (keyCounter.currentKeys < keyCounter.maxKeys)
            {
                keyCounter.ShakeText();
                return;
            }

            gameManager.WinGame();
        }
    }
}