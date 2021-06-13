using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviour : StateMachineBehaviour
{
    public float speed;
    private Object obj;
    private bool toPosA = true;
    private float soundTimeLeft;

    private AudioManager audioManager { get { return FindObjectOfType<AudioManager>(); } }
    private Vector2 patrolPos { get { return toPosA ? obj.PatrolPosA : obj.PatrolPosB; } }
    private Player player { get { return FindObjectOfType<Player>(); } }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soundTimeLeft = 0;
        obj = animator.transform.GetComponent<Object>();
        obj.ObjectLight.SetActive(player.CurrentlyPossessedObject == obj);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(soundTimeLeft <= 0)
        {
            soundTimeLeft = audioManager.Patrol.length;
            audioManager.PlaySound(audioManager.Patrol);
        }
        else
        {
            soundTimeLeft -= Time.deltaTime;
        }
        var patrolPos = toPosA ? obj.PatrolPosA : obj.PatrolPosB;
        bool leftDir = animator.transform.position.x - patrolPos.x > 0;
        obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, leftDir ? 180 : 0, 0);
        if (Vector2.Distance(animator.transform.position, patrolPos) > 0.1f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPos, speed * Time.deltaTime);
        }
        else
        {
            toPosA = !toPosA;
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
