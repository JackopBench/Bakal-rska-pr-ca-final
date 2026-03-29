using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image leftBar;
    public Image rightBar;
    public float fillSpeed;
    public float drainSpeed;

    private bool filling = false;
    private bool hasChased = false;

    public EnemyBase enemy;
    public DDAManager ddaManager; 

    private float chaseTimer = 0f;
    public float minChaseTime = 3f;

    public float minFillSpeed = 3f;
    public float maxFillSpeed = 1f;

    public float minDrainSpeed = 1f; 
    public float maxDrainSpeed = 0.8f;

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
        
        if (hasChased)
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

        
        if (leftBar.fillAmount >= 1f && !hasChased)
        {
            enemy.OnBarFilled();
            hasChased = true;
            chaseTimer = 0f;
        }

        
        if (leftBar.fillAmount <= 0f && hasChased)
{
    enemy.OnBarEmpty();

    if (chaseTimer >= minChaseTime && ddaManager != null)
    {
        ddaManager.OnEnemyEscaped(); 
    }

    hasChased = false;
    chaseTimer = 0f;
}
    }

    public void StartFilling()
    {
        filling = true;
    }

    public void StopFilling()
    {
        filling = false;
    }
}