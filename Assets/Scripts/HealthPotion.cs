using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PotionSpawner spawner;
    public int spawnIndex;
    public DDAManager ddaManager;


    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
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
            
            spawner.PotionTaken(spawnIndex);
            Destroy(gameObject);
        }
    }
}