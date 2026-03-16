using UnityEngine;

public class EnemyLayerZone : MonoBehaviour
{
    public int enterOrder = 1;
    public int exitOrder = 5;

        private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Niečo vstúpilo: " + other.name);

    if (other.CompareTag("Enemy"))
    {
        Debug.Log("Enemy vstupil do zony");
        SetEnemyOrder(other, enterOrder);
    }
}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SetEnemyOrder(other, exitOrder);
        }
    }

    void SetEnemyOrder(Collider2D col, int order)
    {
        // zmení order na všetkých SpriteRenderer v enemy (bez ohľadu kde sú)
        SpriteRenderer[] renderers = col.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in renderers)
        {
            sr.sortingOrder = order;
        }
    }


}