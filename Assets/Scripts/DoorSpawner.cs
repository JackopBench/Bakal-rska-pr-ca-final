using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject doorPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnDoor();
    }

    void SpawnDoor()
    {
        if (spawnPoints.Length == 0 || doorPrefab == null)
        {
            Debug.LogWarning("Missing spawn points or door prefab!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        Instantiate(doorPrefab, chosenPoint.position, chosenPoint.rotation);
    }
}