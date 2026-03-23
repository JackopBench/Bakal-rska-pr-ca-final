using UnityEngine;

public class DDAManager : MonoBehaviour
{
    // DificultyScore premenne
    public float difficultyScore = 50f;
    public float minScore = 0f;
    public float maxScore = 100f;

    // Damage not taken
    public float noDamageThreshold = 15f;
    public float noDamageReward = 5f; 
    public float timeSinceLastHit = 15f;

    // Player escape
    public float escapeReward = 5f;

    // No progress
    public float noProgressThreshold = 20f;
    public float noProgressPenalty = -5f;
    private float timeSinceLastProgress = 20f;
    private int lastDifficulty = -1;

    
    void Update()
    {
        int currentDifficulty = GetDifficultyLevel();

        if (currentDifficulty != lastDifficulty)
        {
            Debug.Log($"[DDA] Difficulty changed: {lastDifficulty} → {currentDifficulty}");
            lastDifficulty = currentDifficulty;
        }

        timeSinceLastHit += Time.deltaTime;
        CheckNoDamageBonus();

        timeSinceLastProgress += Time.deltaTime;
        CheckNoProgressPenalty();

        ClampScore();
    }

    void ClampScore()
    {
        difficultyScore = Mathf.Clamp(difficultyScore, minScore, maxScore);
    }
    
    public void OnKeyCollected()
    {
        difficultyScore += 5f;
        ClampScore();
        timeSinceLastProgress = 0f;
        Debug.Log($"[DDA] Key collected | Difficulty Score: {difficultyScore}");
    }

    public void CheckNoDamageBonus()
    {
        if (timeSinceLastHit >= noDamageThreshold)
        {
            difficultyScore += noDamageReward;
            ClampScore();

            Debug.Log($"[DDA] No damage for {noDamageThreshold}s → +{noDamageReward} | Score: {difficultyScore}");

            timeSinceLastHit = 0f;
        }   
    }

    public void OnDamageTaken()
    {
        float change = -8f;
        difficultyScore += change;
        ClampScore();
        Debug.Log($"Player got hit -> -5, dificulty score = {difficultyScore}");

        timeSinceLastHit = 0f;
    }

    public void OnPotionCollected()
    {
        float change = -8f;
        difficultyScore += change;

        Debug.Log($"[DDA] Potion collected -> {change}, difficulty score = {difficultyScore}");
    }

    public void OnEnemyEscaped()
    {
        difficultyScore += escapeReward;
        ClampScore();

        Debug.Log($"[DDA] Enemy escaped → +{escapeReward} | Score: {difficultyScore}");
    }

    public void CheckNoProgressPenalty()
    {
        if (timeSinceLastProgress >= noProgressThreshold)
        {
            difficultyScore += noProgressPenalty;
            ClampScore();   

            Debug.Log($"[DDA] No progress → {noProgressPenalty} | Score: {difficultyScore}");

            timeSinceLastProgress = 0f;
        }   
    }

    public void OnGameOver()
{
    float change = -30f;
    difficultyScore += change;
    ClampScore();

    Debug.Log($"[DDA] Game Over → {change} | Score: {difficultyScore}");
}

    public int GetDifficultyLevel()
    {
        int level = Mathf.FloorToInt(difficultyScore / 10f) + 1;
        return Mathf.Clamp(level, 1, 10);
    }
}
