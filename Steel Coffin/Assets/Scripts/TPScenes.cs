using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPScenes : MonoBehaviour
{
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

            if (Input.GetKeyDown(KeyCode.E) && playerInventory.keyPickup)
            {
                NextLevel();
            }
        }
        else
        {
            activateText.gameObject.SetActive(false);
        }
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}

