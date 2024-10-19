using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstackleAvoidance : MonoBehaviour
{
    public float detectionRange = 2.0f;
    public float avoidanceStrength = 3.0f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * detectionRange;

        if (Physics.Raycast(transform.position, forward, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Vector3 avoidanceDirection = Vector3.Cross(Vector3.up, hit.normal) * avoidanceStrength;
                agent.SetDestination(transform.position + avoidanceDirection);
            }
        }
    }
}
