using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyCounter keyCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("hitBox"))
        {
            keyCounter.AddKey();
            Destroy(gameObject);
        }
    }
}