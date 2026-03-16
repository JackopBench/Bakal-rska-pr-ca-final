using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    public Transform[] spawnPoints;
    public Transform[] waypoints;

    public int npcCount = 3;

    private List<GameObject> spawnedNPCs = new List<GameObject>();

    void Update()
    {
        // spawn keď je ich málo
        while (spawnedNPCs.Count < npcCount)
        {
            SpawnNPC();
        }

        // zmaž keď je ich veľa
        while (spawnedNPCs.Count > npcCount)
        {
            GameObject npc = spawnedNPCs[0];
            spawnedNPCs.RemoveAt(0);
            Destroy(npc);
        }
    }

    void SpawnNPC()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        GameObject npc = Instantiate(
            npcPrefab,
            spawnPoints[randomIndex].position,
            Quaternion.identity
        );

        // nastav waypointy
        NPCController controller = npc.GetComponent<NPCController>();
        controller.waypoints = waypoints;

        spawnedNPCs.Add(npc);
    }
}