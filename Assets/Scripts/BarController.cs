using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BarController : MonoBehaviour
{
    public Image leftBar;
    public Image rightBar;

    public float fillSpeed;
    public float drainSpeed;

    private bool filling = false;

    public DDAManager ddaManager;

    private float chaseTimer = 0f;
    public float minChaseTime = 3f;

    public float minFillSpeed = 1f;
    public float maxFillSpeed = 3f;

    public float minDrainSpeed = 0.8f;
    public float maxDrainSpeed = 1f;

    private List<EnemyBase> enemies = new List<EnemyBase>();
    private List<EnemyBase> chasingEnemies = new List<EnemyBase>();

    void Start()
    {
        leftBar.fillAmount = 0f;
        rightBar.fillAmount = 0f;
    }

    void Update()
    {
        if (ddaManager == null)
            ddaManager = DDAManager.instance;

        if (ddaManager != null)
        {
            int difficulty = ddaManager.GetDifficultyLevel();
            float t = (difficulty - 1) / 9f;

            fillSpeed = Mathf.Lerp(minFillSpeed, maxFillSpeed, t);
            drainSpeed = Mathf.Lerp(minDrainSpeed, maxDrainSpeed, t);
        }

        if (chasingEnemies.Count > 0)
        {
            chaseTimer += Time.deltaTime;
        }

        
        if (filling)
        {
            leftBar.fillAmount += fillSpeed * Time.deltaTime;
            rightBar.fillAmount += fillSpeed * Time.deltaTime;
        }
        else
        {
            leftBar.fillAmount -= drainSpeed * Time.deltaTime;
            rightBar.fillAmount -= drainSpeed * Time.deltaTime;
        }

        leftBar.fillAmount = Mathf.Clamp01(leftBar.fillAmount);
        rightBar.fillAmount = Mathf.Clamp01(rightBar.fillAmount);


       
        if (leftBar.fillAmount >= 1f)
        {
            foreach (EnemyBase enemy in enemies)
            {
                if (!chasingEnemies.Contains(enemy))
                {
                    enemy.OnBarFilled();
                    chasingEnemies.Add(enemy);
                }
            }
        }


       
        if (leftBar.fillAmount <= 0f && chasingEnemies.Count > 0)
        {
            foreach (EnemyBase enemy in chasingEnemies)
            {
                enemy.OnBarEmpty();
            }

            chasingEnemies.Clear();

            if (chaseTimer >= minChaseTime && ddaManager != null)
            {
                ddaManager.OnEnemyEscaped();
            }

            chaseTimer = 0f;
        }
    }


    public void StartFilling(EnemyBase enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);

        filling = true;
    }


    public void StopFilling(EnemyBase enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);

        if (enemies.Count == 0)
            filling = false;
    }
}