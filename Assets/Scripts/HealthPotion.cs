using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                bool healed = health.Heal();

                if (healed)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}