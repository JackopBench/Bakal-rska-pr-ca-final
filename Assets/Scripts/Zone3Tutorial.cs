using UnityEngine;

public class Zone3Tutorial : MonoBehaviour
{
    public GameObject healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthBar.SetActive(true);
        }
    }
}