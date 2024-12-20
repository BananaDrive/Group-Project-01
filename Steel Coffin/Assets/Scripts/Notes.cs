using UnityEngine;
using TMPro;

public class RadiusInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 5f; 
    public KeyCode interactionKey = KeyCode.E; 

    [Header("References")]
    public TextMeshProUGUI interactionText; 
    public Transform player; 

    private bool isPlayerInRange = false;

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Interaction Text is not assigned!");
        }
    }

    private void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= radius)
        {
            isPlayerInRange = true;

            
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
                interactionText.gameObject.SetActive(false); 
            }
        }
    }

    
}
