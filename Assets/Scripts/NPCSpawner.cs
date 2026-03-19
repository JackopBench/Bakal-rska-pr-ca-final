using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    public Transform[] spawnPoints;
    public Transform[] waypoints;

    public int npcCount = 3;
    public DDAManager ddaManager;
    public int minNPC = 1;
    public int maxNPC = 4;
    private List<GameObject> spawnedNPCs = new List<GameObject>();

    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
    }

    void Update()
    {
        npcCount = GetTargetNPCCount();
        
        while (spawnedNPCs.Count < npcCount)
        {
            SpawnNPC();
        }

        
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

    NPCController controller = npc.GetComponent<NPCController>();

    
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

    int GetTargetNPCCount()
    {
        if (ddaManager == null) return npcCount;

        int difficulty = ddaManager.GetDifficultyLevel();

        float t = (difficulty - 1) / 9f;

        float value = Mathf.Lerp(minNPC, maxNPC, t);

        return Mathf.RoundToInt(value);
    }
}