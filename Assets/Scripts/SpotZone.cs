using UnityEngine;

public class SpotZone : MonoBehaviour
{
    public BarController barController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            barController.StartFilling();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            barController.StopFilling();
    }
}