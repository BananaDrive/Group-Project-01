using UnityEngine;

public class DistractionAnimation : StateMachineBehaviour
{
    private Animator animmator;

    private int Distractionloops = 3;
    private int currentLoop = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FuseTrigger"))
        {
            animator.SetTrigger("TriggerFuse");
        }
    }

    void OnDistractionLoopComplete()
    {
        CurrentLoop++;
    }
}
