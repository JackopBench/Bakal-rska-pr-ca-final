using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PotionSpawner spawner;
    public int spawnIndex;
    public DDAManager ddaManager;

    public AudioSource pickupSound;


    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
        pickupSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("hitBox")) return;

        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
        if (health == null) return;

        bool healed = health.Heal();

        if (healed)
        {
            ddaManager.OnPotionCollected();
            
            AudioSource.PlayClipAtPoint(
            pickupSound.clip,
            transform.position,
            SFXManager.instance.sfxVolume
        );
            
            spawner.PotionTaken(spawnIndex);
            Destroy(gameObject);
        }
    }
}