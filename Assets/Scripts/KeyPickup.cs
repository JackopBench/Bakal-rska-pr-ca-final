using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyCounter keyCounter;
    public DDAManager DDAManager;
    public AudioSource pickupSound;

    void Start()
    {
        keyCounter = FindFirstObjectByType<KeyCounter>();
        DDAManager = FindFirstObjectByType<DDAManager>();
        pickupSound = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            keyCounter.AddKey();
            
            AudioSource.PlayClipAtPoint(
            pickupSound.clip,
            transform.position,
            SFXManager.instance.sfxVolume
        );
            DDAManager.OnKeyCollected();
            Destroy(gameObject);
        }
    }
}