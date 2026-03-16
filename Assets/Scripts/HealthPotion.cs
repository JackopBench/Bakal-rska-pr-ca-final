using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PotionSpawner spawner;
    public int spawnIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("hitBox")) return;

        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
        if (health == null) return;

        bool healed = health.Heal();

        // potion sa zoberie iba ak sa hráč naozaj healne
        if (healed)
        {
            spawner.PotionTaken(spawnIndex);
            Destroy(gameObject);
        }
    }
}