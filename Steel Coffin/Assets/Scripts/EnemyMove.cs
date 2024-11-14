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
    public float searchRoamTime = 10f; // Time to roam around last seen position
    public Vector3 roamCenter; // Static roam area (e.g., center of the map)

    public Transform player;
    public LayerMask obstacleLayer; // Layer mask to specify obstacles

    private NavMeshAgent Ai;
    private Player playerScript;
    private float DistanceToPlayer;
    private Vector3 lastSeenPosition;
    private Vector3 roamPoint;
    private float searchTimer;
    private bool isSearching;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        Ai = GetComponent<NavMeshAgent>();
        isSearching = false;
        searchTimer = searchRoamTime;
    }

    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (DistanceToPlayer <= vision && HasLineOfSight())
        {
            // Chase the player
            Ai.destination = player.position;
            Ai.isStopped = false;
            lastSeenPosition = player.position; // Update last seen position
            isSearching = true; // Start search mode when the player goes out of range
            searchTimer = searchRoamTime; // Reset search timer
        }
        else if (isSearching)
        {
            // Roam around the last seen position for a specified time
            RoamAroundLastSeenPosition();
        }
        else
        {
            // Roam around a fixed area (e.g., the center of the map)
            RoamAroundStaticArea();
        }
    }

    // Check if the enemy has a clear line of sight to the player
    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Perform a raycast to check for obstacles
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstacleLayer))
        {
            // If the ray hits something before reaching the player, there's an obstacle in the way
            return hit.transform == player;
        }

        // If no obstacles were hit, there's a clear line of sight
        return true;
    }

    // Roam around the last seen position
    void RoamAroundLastSeenPosition()
    {
        if (Vector3.Distance(transform.position, roamPoint) < 1f || Ai.isStopped)
        {
            SetRandomRoamPoint(lastSeenPosition);
        }
        Ai.destination = roamPoint;

        // Count down the timer for searching
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0)
        {
            isSearching = false; // Stop searching after the timer expires
        }
    }

    // Roam around a static area (like the center of the map)
    void RoamAroundStaticArea()
    {
        if (Vector3.Distance(transform.position, roamPoint) < 1f || Ai.isStopped)
        {
            SetRandomRoamPoint(roamCenter);
        }
        Ai.destination = roamPoint;
    }

    // Set a random point within a specified radius
    void SetRandomRoamPoint(Vector3 center)
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += center;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, roamRadius, NavMesh.AllAreas))
        {
            roamPoint = navHit.position;
        }
    }
}