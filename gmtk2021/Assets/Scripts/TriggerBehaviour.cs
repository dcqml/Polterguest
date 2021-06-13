using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : StateMachineBehaviour
{
    private Object obj;
    public float speed;

    private AudioManager audioManager { get { return FindObjectOfType<AudioManager>(); } }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        obj = animator.transform.GetComponent<Object>();
        audioManager.PlaySound(audioManager.Trigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float patrolXPos = obj.TriggerSource.transform.position.x;
        bool leftDir = animator.transform.position.x - patrolXPos > 0;
        obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, leftDir ? 180 : 0, 0);

        if (Vector2.Distance(animator.transform.position, new Vector2(patrolXPos, animator.transform.position.y)) > 0.2f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(patrolXPos, animator.transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isIdlePostTrigger", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
