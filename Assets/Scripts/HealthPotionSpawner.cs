using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionSpawner : MonoBehaviour
{
    public GameObject potionPrefab;
    public Transform[] spawnPoints;

    public int maxPotions = 3;
    public float respawnTime = 10f;

    private List<int> activeSpawnPoints = new List<int>();

    void Start()
    {
        for (int i = 0; i < maxPotions; i++)
        {
            SpawnRandomPotion();
        }
    }

    void SpawnRandomPotion()
    {
        List<int> available = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!activeSpawnPoints.Contains(i))
                available.Add(i);
        }

        if (available.Count == 0) return;

        int randomIndex = available[Random.Range(0, available.Count)];

        GameObject potion = Instantiate(potionPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

        HealthPotion hp = potion.GetComponentInChildren<HealthPotion>();
        hp.spawner = this;
        hp.spawnIndex = randomIndex;

        activeSpawnPoints.Add(randomIndex);
    }

    public void PotionTaken(int index)
    {
        activeSpawnPoints.Remove(index);
        StartCoroutine(RespawnPotion());
    }

    IEnumerator RespawnPotion()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnRandomPotion();
    }
}