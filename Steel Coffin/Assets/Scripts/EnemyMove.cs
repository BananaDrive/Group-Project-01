using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public float vision;
    public float roamRadius;
    public float searchRadius;
    public float searchRoamTime;
    public float waitTimeAtPoint;
    public Vector3 roamCenter;

    public Transform[] players;
    public LayerMask obstacleLayer;
    public LayerMask InvisableLayer;

    private NavMeshAgent Ai;
    private Player playerScript;
    private float DistanceToPlayer;
    private Vector3 lastSeenPosition;
    private Vector3 roamPoint;
    private float searchTimer;
    private bool isSearching;
    private bool isWaiting;
    private float waitTimer;

    private Quaternion fixedRotor;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Ai = GetComponent<NavMeshAgent>();
        isSearching = false;
        isWaiting = false;
        searchTimer = searchRoamTime;
        waitTimer = waitTimeAtPoint;

        roamPoint = Vector3.zero;

        if (roamCenter == Vector3.zero)
        {
            roamCenter = transform.position;
        }

        fixedRotor = transform.rotation;
    }

    void Update()
    {
        transform.rotation = fixedRotor;
        
        players = GameObject.FindGameObjectsWithTag("Player").Select(player => player.transform).ToArray();

        float closestDistance = float.MaxValue; 
        Transform closestPlayer = null;


        int playerLayer = LayerMask.NameToLayer("player"); 

        foreach (Transform player in players)
        {
            if (player.gameObject.layer != playerLayer)
                continue;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= vision && HasLineOfSight(player))
            {
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = player;
                }
            }
        }


        if (closestPlayer != null)
        {
            
            Ai.destination = closestPlayer.position;
            Ai.isStopped = false;
            lastSeenPosition = closestPlayer.position;
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
    }

    bool HasLineOfSight(Transform player)
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit Hit, distanceToPlayer, obstacleLayer))
        {
            if (((1 << Hit.transform.gameObject.layer) & InvisableLayer) != 0)
            {
                return false;
            }

            return Hit.transform == player;
        }
        return true;
    }

    

    void RoamStaticCenter()
    {

            

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
                    SetRandomRoamPoint(roamCenter, false);
                    }
                }
            }
            Ai.isStopped = false;
            Ai.destination = roamPoint;
        
    }

    void RoamLastPosition()
    {

        

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
                    SetRandomRoamPoint(lastSeenPosition, true);
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

    void SetRandomRoamPoint(Vector3 center, bool useRadius)
    {
        int maxAttempts = 10;
        float radius = useRadius ? searchRadius : roamRadius;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, NavMesh.AllAreas))
            {
                roamPoint = navHit.position;
                return;
            }
        }
       
    }

    private void OnDrawGizmos()
    {
        if (Ai != null && Ai.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Ai.destination);
        }
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, vision);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(roamCenter, roamRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lastSeenPosition, searchRadius);
    }
}
