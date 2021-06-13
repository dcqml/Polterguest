using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateBehaviour : StateMachineBehaviour
{
    private Object obj;
    private Animation anim;

    public float TranslateSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        obj = animator.GetComponent<Object>();
        anim = animator.GetComponent<Animation>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Vector2 target = translateX ? new Vector2(obj.InteractionPosDest.x, animator.transform.position.y) : new Vector2(animator.transform.position.x, obj.InteractionPosDest.y);
        Vector2 target = obj.InteractionPosDest;
        float dist = Vector2.Distance(new Vector2(animator.transform.position.x, animator.transform.position.y), target);
        if(dist > 0.1f)
        {
            var movement = Vector2.MoveTowards(animator.transform.position, target, TranslateSpeed * Time.deltaTime);
            animator.transform.position = movement;
        }
        else
        {
            animator.transform.position = target;
            animator.SetBool("isTranslating", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var tmp = obj.OriginPos;
        obj.OriginPos = obj.InteractionPosDest;
        obj.InteractionPosDest = tmp;
    }
}
