using UnityEngine;

public class playermoveanimations : StateMachineBehaviour
{
    private Rigidbody rb;     
    private float walkThreshold = 0.001f; 
    private bool isInitialized = false; 

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        if (!isInitialized)
        {
            
            rb = animator.GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogError("Rigidbody2D not found on the GameObject!");
                return;
            }

            isInitialized = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

        if (rb == null) return;

       
        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);

        
        bool isWalking = horizontalSpeed > walkThreshold;

       
        animator.SetBool("IsWalking", isWalking);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.SetBool("IsWalking", false); 
    }
}
