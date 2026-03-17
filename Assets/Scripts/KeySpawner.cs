using UnityEngine;
using System.Collections.Generic;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;

    public int keysToSpawn = 6;

    void Start()
    {
        List<int> usedIndexes = new List<int>();

        while (usedIndexes.Count < keysToSpawn)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);

            if (!usedIndexes.Contains(randomIndex))
            {
                usedIndexes.Add(randomIndex);


                Instantiate(
                    keyPrefab,
                    spawnPoints[randomIndex].position,
                    Quaternion.identity
                );
            }
        }
    }
}