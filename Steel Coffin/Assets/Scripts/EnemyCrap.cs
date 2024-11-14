using UnityEngine;
using UnityEngine.AI;

public class EnemyCrap : MonoBehaviour
{
    public Player player;
    public NavMeshAgent agent;

    public float VisionRange = 5.0f;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (DistanceToPlayer <= VisionRange)
        {
            agent.destination = Player.position;
            agent.isStopped = false;
        }

        else
            agent.isStopped = true;
    }
}
