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
        if (Vector3.Distance(transform.position, roamPoint) < 1f || Ai.isStopped)
        {
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
        if (Vector3.Distance(transform.position, roamPoint) < 1f || Ai.isStopped)
        {
            SetRandomRoamPoint(roamCenter);
        }
        Ai.destination = roamPoint;
    }

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