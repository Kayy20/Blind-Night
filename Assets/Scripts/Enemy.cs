using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Bounds boundaries;

    private NavMeshAgent agent;

    [SerializeField]
    private List<Vector3> roamingPoints = new List<Vector3>();
    private int currentRoamingPointHeadTo;
    public int numOfRoamingPoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < numOfRoamingPoints; i++)
        {
            roamingPoints.Add(new Vector3(Random.Range(boundaries.min.x, boundaries.max.x), Random.Range(boundaries.min.y, boundaries.max.y), Random.Range(boundaries.min.z, boundaries.max.z)));
        }
    }

    private void Update()
    {
        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    NextDestination();
                }
            }
        }
    }

    private void NextDestination()
    {
        currentRoamingPointHeadTo = currentRoamingPointHeadTo == numOfRoamingPoints - 1 ? 0 : currentRoamingPointHeadTo + 1;

        agent.SetDestination(roamingPoints[currentRoamingPointHeadTo]);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ReloadScene();
        }
    }

}
