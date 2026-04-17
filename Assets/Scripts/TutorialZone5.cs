using UnityEngine;

public class TutorialZone5 : MonoBehaviour
{
    public GameObject key;
    public GameObject keyCounter;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            key.SetActive(true);
            keyCounter.SetActive(true);
        }
    }
}