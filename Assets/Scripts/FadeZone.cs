using UnityEngine;

public class FadeZone : MonoBehaviour
{
    public GameObject[] objectsToFade;
    public float fadedAlpha = 0.3f;

    public int playerEnterOrder = 0;
    public int playerExitOrder = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetAlpha(fadedAlpha);
            SetPlayerOrder(other, playerEnterOrder);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetAlpha(1f);
            SetPlayerOrder(other, playerExitOrder);
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (GameObject obj in objectsToFade)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
        }
    }

    void SetPlayerOrder(Collider2D col, int order)
    {
        SpriteRenderer sr = col.GetComponent<SpriteRenderer>();

        if (sr == null)
            sr = col.GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
            sr.sortingOrder = order;
    }
}