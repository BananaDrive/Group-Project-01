using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Animator leveranimator;
    public float ActivateRng = 3.0f;
    public Transform player;

    private PlayerInventory playerInventory;

    private bool isFlipping = false;
    public AudioSource Lever;

    private void Start()
    {
        playerInventory = player.GetComponent<PlayerInventory>();
    }
    void Update()
    {
        if (isFlipping) return;

        if (Vector3.Distance(transform.position, player.position)<=ActivateRng)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Lever.Play();
                TriggerFlip();
                playerInventory.keyPickup = true;
            }
        }
    }

    void TriggerFlip()
    {
        isFlipping = true;
        leveranimator.SetTrigger("FlipLever");
    }
    
}
