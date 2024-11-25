using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPScenes : MonoBehaviour
{
    public float activationRange = 1f;
    public Transform player;
    public TextMeshProUGUI activateText;
    private PlayerInventory playerInventory;
    public GameManager GM;

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

            if (Input.GetKeyDown(KeyCode.E) && playerInventory.keyPickup)
            {
                GM.LoadNextLevel();
            }
        }
        else
        {
            activateText.gameObject.SetActive(false);
        }
    }

    

}

