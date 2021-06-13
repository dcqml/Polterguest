using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Initialization
    
    public bool IsEndObject = false;
    public bool IsPossessed { get { return Object_Animator.GetBool("isPossessed"); } }

    public bool CanPatrol = false;
    public bool Reversible = true;

    public InteractionType InteractionType = InteractionType.Flip;
    public Vector2 OriginPos;
    public Vector2 InteractionPosDest;
    public GameObject InteractionPosDestObj;
    public GameObject AnimObject;
    public GameObject PlayerJointPosition;
    public GameObject PatrolPosAObj;
    public GameObject PatrolPosBObj;
    public Vector2 PatrolPosA;
    public Vector2 PatrolPosB;

    private Player player { get { return FindObjectOfType<Player>(); } }

    private bool ActivatedOnce = false;
    private Animator Object_Animator;

    public List<GameObject> TriggerObjects; // Objects that can be triggered on this object activation
    public Object TriggerSource; // Object that triggered the other object
    public bool alreadyTriggered = false;

    public GameObject ObjectLight;

    public void Start()
    {
        OriginPos = transform.position;
        Object_Animator = GetComponent<Animator>();
        InteractionPosDest = InteractionPosDestObj.transform.position;
        PatrolPosA = PatrolPosAObj.transform.position;
        PatrolPosB = PatrolPosBObj.transform.position;
        if (CanPatrol) Patrol();
    }

    public void Update()
    {
        bool closeEnough = Vector2.Distance(player.transform.position, transform.position) <= player.PossessionRadius;
        ObjectLight.SetActive(closeEnough && (!Object_Animator.GetBool("isPatrolling") || Object_Animator.GetBool("isIdlePostTrigger")));
    }

    // Activation function
    public void Activate()
    {
        if(IsPossessed)
        {
            if (Reversible || (!Reversible && !ActivatedOnce))
            {
                Camera.main.GetComponent<RipplePostProcessor>().Ripple(PlayerJointPosition.transform.position);
                Object_Animator.SetTrigger(InteractionType == InteractionType.Flip ? "flip" : "translate");
                foreach (var obj in TriggerObjects)
                {
                    var objComp = obj.GetComponent<Object>();
                    if(objComp != null && !alreadyTriggered)
                    {
                        objComp.TriggerSource = this;
                        objComp.Trigger();
                    }
                    alreadyTriggered = true;
                }
                if (!Reversible) ActivatedOnce = true;
            }
            else if(!Reversible && ActivatedOnce)
            {
                print("already activated once!");
            }
        }
    }

    public void SetPossess(bool possess)
    {
        Object_Animator.SetBool("isPossessed", possess);
        if(possess) Camera.main.GetComponent<RipplePostProcessor>().Ripple(PlayerJointPosition.transform.position);
    }
    
    // Patrol function
    public void Patrol()
    {
        Object_Animator.SetBool("isPatrolling", true);
    }

    // Trigger function
    public void Trigger()
    {
        Object_Animator.SetTrigger("trigger");
    }
}

public enum InteractionType
{
    Flip,
    Translate
}