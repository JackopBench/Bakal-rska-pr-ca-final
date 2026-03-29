using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner2 : MonoBehaviour
{
    public GameObject npcPrefab;

    public Transform[] spawnPoints;
    public Transform[] waypoints;


    private List<GameObject> spawnedNPCs = new List<GameObject>();

    void Start()
    {
        SpawnNPC();
        SpawnNPC();
    }

    void SpawnNPC()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        GameObject npc = Instantiate(
            npcPrefab,
            spawnPoints[randomIndex].position,
            Quaternion.identity
        );

        NPCController2 controller = npc.GetComponent<NPCController2>();

        Transform[] shuffledWaypoints = new Transform[waypoints.Length];
        waypoints.CopyTo(shuffledWaypoints, 0);

        for (int i = 0; i < shuffledWaypoints.Length; i++)
        {
            int rnd = Random.Range(i, shuffledWaypoints.Length);
            Transform temp = shuffledWaypoints[i];
            shuffledWaypoints[i] = shuffledWaypoints[rnd];
            shuffledWaypoints[rnd] = temp;
        }

        controller.waypoints = shuffledWaypoints;

        spawnedNPCs.Add(npc);
    }
}