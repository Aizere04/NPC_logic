using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPC_behaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Transform> checkpoints; // Список чекпоинтов
    private Transform currentTarget;
    public float detectionRange = 5.0f; // Дальность обнаружения препятствий
    public LayerMask obstacleLayer; // Слой для обнаружения препятствий

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomCheckpoint();
    }

    void Update()
    {
        AvoidObstacles();
        MoveToCheckpoint();
    }

    void SetRandomCheckpoint()
    {
        if (checkpoints.Count == 0)
            return;

        // Выбираем случайный чекпоинт из списка
        int randomIndex = Random.Range(0, checkpoints.Count);
        currentTarget = checkpoints[randomIndex];
        agent.SetDestination(currentTarget.position);
    }

    void MoveToCheckpoint()
    {
        // Проверяем, достиг ли агент своей цели
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomCheckpoint();
        }
    }

    void AvoidObstacles()
    {
        // Обнаружение препятствий перед NPC с использованием Raycast
        RaycastHit hit;
        Vector3 direction = agent.transform.forward;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange, obstacleLayer))
        {
            // Если обнаружено препятствие, изменить направление
            Vector3 avoidanceDirection = Vector3.Cross(Vector3.up, hit.normal).normalized;
            Vector3 newDestination = transform.position + avoidanceDirection * detectionRange;
            NavMeshHit navHit;

            // Проверяем, что новая точка назначения находится на NavMesh
            if (NavMesh.SamplePosition(newDestination, out navHit, detectionRange, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
            }
        }
    }
    public float playerDetectionRange = 10.0f; // Радиус обнаружения игрока
    public Transform playerTransform; // Ссылка на игрока

   
    

}
