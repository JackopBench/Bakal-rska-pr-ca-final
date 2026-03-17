using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyCounter keyCounter;
    public DDAManager DDAManager;

    void Start()
    {
        keyCounter = FindFirstObjectByType<KeyCounter>();
        DDAManager = FindFirstObjectByType<DDAManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            keyCounter.AddKey();
            DDAManager.OnKeyCollected();
            Destroy(gameObject);
        }
    }
}