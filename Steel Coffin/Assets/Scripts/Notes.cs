using UnityEngine;
using TMPro;

public class RadiusInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 5f; // The radius around the object
    public KeyCode interactionKey = KeyCode.E; // Key to activate interaction

    [Header("References")]
    public TextMeshProUGUI interactionText; // Reference to the TMP UI element
    public Transform player; // Reference to the player

    private bool isPlayerInRange = false;

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Ensure the text is initially hidden
        }
        else
        {
            Debug.LogWarning("Interaction Text is not assigned!");
        }
    }

    private void Update()
    {
        // Check the distance between the object and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= radius)
        {
            isPlayerInRange = true;

            // Check if the interaction key is pressed
            if (Input.GetKeyDown(interactionKey))
            {
                interactionText.gameObject.SetActive(true);
            }
        }
        else
        {
            isPlayerInRange = false;
            if (interactionText != null && interactionText.gameObject.activeSelf)
            {
                interactionText.gameObject.SetActive(false); // Hide the TMP text
            }
        }
    }

    
}
