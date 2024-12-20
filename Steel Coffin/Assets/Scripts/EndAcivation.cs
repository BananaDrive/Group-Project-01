using UnityEngine;

public class EndAcivation : MonoBehaviour
{
    public GameManager Gm;
    PlayerInventory playerInventory;
    public Transform player;
    public Animator EndCutScene;


    void Start()
    {
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    
    void Update()
    {
        
    }
}
