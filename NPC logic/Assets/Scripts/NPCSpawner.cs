using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class NPCSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints; // Точки спавна
    public List<GameObject> npcPrefabs; // Префабы разных NPC
    public int npcCount = 5; // Количество NPC для спавна
    public List<Transform> checkpoints; // Список чекпоинтов для каждого NPC
    public LayerMask obstacleLayer; // Слой препятствий для NPC

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            int randomPrefabIndex = Random.Range(0, npcPrefabs.Count);
            GameObject npcPrefab = npcPrefabs[randomPrefabIndex];

            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

            // Добавляем NavMeshAgent и NPC_behaviour, если их нет на префабе
            if (npc.GetComponent<NavMeshAgent>() == null)
            {
                npc.AddComponent<NavMeshAgent>();
            }

            NPC_behaviour npcBehaviour = npc.GetComponent<NPC_behaviour>();
            if (npcBehaviour == null)
            {
                npcBehaviour = npc.AddComponent<NPC_behaviour>();
            }

            npcBehaviour.checkpoints = checkpoints;
            npcBehaviour.obstacleLayer = obstacleLayer;
        }
    }
}

