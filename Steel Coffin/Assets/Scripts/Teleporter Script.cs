using UnityEngine;
using TMPro;

public class TeleporterScript : MonoBehaviour
{
    public Vector3 teleportPos = new Vector3(0, 0, 0);
    public float activationRange = 1f;
    public Transform player;
    public TextMeshProUGUI activateText;
    private PlayerInventory playerInventory;

    void Start()
    {
 
        playerInventory = player.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory component not found on player.");
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= activationRange)
        {
            activateText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = teleportPos;
            }
        }
        else
        {
            activateText.gameObject.SetActive(false);
        }
    }
}
