using UnityEngine;

public class DoorWinTrigger : MonoBehaviour
{
    public KeyCounter keyCounter;
    public WinManager winManager;

    private bool playerInRange = false;

    private void Start()
    {
        if (keyCounter == null)
            keyCounter = FindFirstObjectByType<KeyCounter>();

        if (winManager == null)
            winManager = FindFirstObjectByType<WinManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (keyCounter.currentKeys >= 9)
            {
                winManager.WinGame();
            }
        }
    }
}