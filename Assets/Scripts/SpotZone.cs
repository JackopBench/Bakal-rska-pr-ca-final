using UnityEngine;

public class SpotZone : MonoBehaviour
{
    private BarController barController;
    private NPCController npcController;

    void Start()
    {
        barController = FindFirstObjectByType<BarController>();
        npcController = GetComponentInParent<NPCController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barController.npcController = npcController;
            barController.StartFilling();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barController.StopFilling();
        }
    }
}