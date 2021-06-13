using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBehaviour : StateMachineBehaviour
{
    private Object obj;

    private bool flipped = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        obj = animator.GetComponent<Object>();
        flipped = !flipped;
        animator.SetBool("isFlipping", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //obj.AnimObject.GetComponent<SpriteRenderer>().flipX = flipped;
        obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, flipped ? 180 : 0, 0);
        animator.SetBool("isFlipping", false);
    }
}
