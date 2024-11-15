using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float Speed = 5f;
    public float vision = 50f;
    public float roamRadius = 20f;
    public float searchRoamTime = 10f;
    public float waitTimeAtPoint = 3f; 
    public Vector3 roamCenter;

    public Transform player;
    public LayerMask obstacleLayer;

    private NavMeshAgent Ai;
    private Player playerScript;
    private float DistanceToPlayer;
    private Vector3 lastSeenPosition;
    private Vector3 roamPoint;
    private float searchTimer;
    private bool isSearching;
    private bool isWaiting;
    private float waitTimer;

    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<Player>();
        Ai = GetComponent<NavMeshAgent>();
        isSearching = false;
        isWaiting = false;
        searchTimer = searchRoamTime;
        waitTimer = waitTimeAtPoint;
    }

    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (DistanceToPlayer <= vision && HasLineOfSight())
        {
            Ai.destination = player.position;
            Ai.isStopped = false;
            lastSeenPosition = player.position;
            isSearching = true;
            searchTimer = searchRoamTime;
        }
        else if (isSearching)
        {
            RoamLastPosition();
        }
        else
        {
            RoamStaticCenter();
        }

        Debug.DrawLine(transform.position, Ai.destination, Color.red);
        Debug.Log($"Roam Center: {roamCenter}");
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit Hit, distanceToPlayer, obstacleLayer))
        {
            return Hit.transform == player;
        }
        return true;
    }

    void RoamLastPosition()
    {
        Debug.Log("Attempting to roam last seen position");

        if (Vector3.Distance(transform.position, roamPoint) < 1.5f || Ai.isStopped)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                waitTimer = waitTimeAtPoint;
            }

            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    SetRandomRoamPoint(lastSeenPosition);
                    isWaiting = false;
                }
            }
        }
        Ai.destination = roamPoint;

        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0)
        {
            isSearching = false;
        }
    }

    void RoamStaticCenter()
    {
        Debug.Log("Attempting to roam around static center");

        if (Vector3.Distance(transform.position, roamPoint) < 1.5f || Ai.isStopped)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                waitTimer = waitTimeAtPoint;
            }

            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    isWaiting = false;
                    SetRandomRoamPoint(roamCenter);
                }
            }
        }
        Ai.isStopped = false;
        Ai.destination = roamPoint;
    }

    void SetRandomRoamPoint(Vector3 center)
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += center;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, roamRadius, NavMesh.AllAreas))
            {
                roamPoint = navHit.position;
                Debug.Log($"Found Roam Point: {roamPoint}");
                return;
            }
        }
        Debug.LogWarning("Unable to find a roam point after multiple attempts.");
    }

    private void OnDrawGizmos()
    {
        if (Ai != null && Ai.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Ai.destination);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(roamCenter, roamRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lastSeenPosition, roamRadius);
    }
}