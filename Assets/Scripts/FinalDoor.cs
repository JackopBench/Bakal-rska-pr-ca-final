using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public GameObject winPanel;
    public KeyCounter keyCounter;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (keyCounter.currentKeys >= keyCounter.maxKeys)
            {
                winPanel.SetActive(true);
            }
            else
            {
                keyCounter.ShakeText();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}   