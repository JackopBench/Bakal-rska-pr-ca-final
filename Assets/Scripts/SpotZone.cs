using UnityEngine;

public class SpotZone : MonoBehaviour
{
    private BarController barController;
    private EnemyBase enemy;

    void Start()
    {
        barController = FindFirstObjectByType<BarController>();
        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barController.StartFilling(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barController.StopFilling(enemy);
        }
    }
}