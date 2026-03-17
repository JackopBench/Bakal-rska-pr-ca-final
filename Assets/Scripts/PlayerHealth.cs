using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hpBars;
    public int health = 3;
    private DDAManager ddaManager;

    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
    }


    public void TakeDamage()
    {
        health--;
        

        if (health >= 0 && health < hpBars.Length)
        {
            hpBars[health].enabled = false;
        }

        if (ddaManager != null)
        {
            ddaManager.OnDamageTaken();
        }
    }

    public bool Heal()
    {
        if (health >= hpBars.Length)
            return false;

        hpBars[health].enabled = true;
        health++;

        return true;
    }
}