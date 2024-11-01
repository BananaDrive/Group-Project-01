using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       
    public float followDistance = 10f; 
    public float stoppingDistance = 2f;
         
    public float sightRange = 10f;    
    public float fieldOfView = 90f;   
    public LayerMask obstacleMask;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= sightRange && angleToPlayer <= fieldOfView / 2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, sightRange, obstacleMask))
            {
                if (hit.transform == player)
                {
                    navMeshAgent.SetDestination(player.position);
                }
            }
        }
    }
}
