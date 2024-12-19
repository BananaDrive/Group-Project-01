using UnityEngine;

public class EnemyAnimation : StateMachineBehaviour
{
    private Rigidbody rb;
    private float walkThreshold = 0f;
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


        float HSpeed = Mathf.Abs(rb.linearVelocity.x);
        float VSpeed = Mathf.Abs(rb.linearVelocity.z);


        bool isWalking = (HSpeed > walkThreshold || VSpeed > walkThreshold);



        animator.SetBool("IsWalking", isWalking);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("IsWalking", false);
    }
}
