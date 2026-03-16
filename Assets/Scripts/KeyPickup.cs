using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyCounter keyCounter;

    void Start()
    {
        keyCounter = FindFirstObjectByType<KeyCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            keyCounter.AddKey();
            Destroy(gameObject);
        }
    }
}