using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public Vector3 teleportPos = new Vector3(0, 0, 0);
    public float activationRange = 1f;
    public Transform player;

    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) <= activationRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = teleportPos;
            }
        }

        
    }
}
