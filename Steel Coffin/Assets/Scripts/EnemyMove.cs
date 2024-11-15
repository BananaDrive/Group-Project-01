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
    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<Player>();
        Ai = GetComponent<NavMeshAgent>();
        isSearching = false;
        searchTimer = searchRoamTime;
    }

    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(transform.position, player.position);

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

        if (Vector3.Distance(transform.position, roamPoint) < 10f || Ai.isStopped)
        {
            Debug.Log("Setting new roam point near last seen position");
            SetRandomRoamPoint(lastSeenPosition);
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

        if (Vector3.Distance(transform.position, roamPoint) < 10f || Ai.isStopped)
        {
            Debug.Log("Setting new roam point near static center");
            SetRandomRoamPoint(roamCenter);
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
                //Debug.Log($"Found Roam Point: {roamPoint}");
                return; // Exit the function if a valid point is found
            }
            else
            {
                //Debug.LogWarning($"Failed to find a roam point near {randomDirection}");
            }
        }
        //Debug.LogWarning("Unable to find a roam point after multiple attempts.");
    }
    private void OnDrawGizmos()
    {
        if (Ai != null && Ai.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Ai.destination);
        }

        // Optional: Draw the roam radius for visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(roamCenter, roamRadius);
    }
}