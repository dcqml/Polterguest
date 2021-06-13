using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Initialization
    private AudioManager audioManager { get { return FindObjectOfType<AudioManager>(); } }
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

    public void Awake()
    {
        OriginPos = transform.position;
        Object_Animator = GetComponent<Animator>();
        InteractionPosDest = InteractionPosDestObj.transform.position;
        PatrolPosA = PatrolPosAObj.transform.position;
        PatrolPosB = PatrolPosBObj.transform.position;
    }

    public void Start()
    {
        if (CanPatrol) Patrol();
    }

    public void Update()
    {
        float dist = Vector2.Distance(player.transform.position, PlayerJointPosition.transform.position);
        bool closeEnough = dist <= player.PossessionRadius;
        ObjectLight.SetActive(closeEnough && (!Object_Animator.GetBool("isPatrolling") || Object_Animator.GetBool("isIdlePostTrigger"))
            && !Object_Animator.GetBool("isTranslating") && !Object_Animator.GetBool("isFlipping"));
    }

    // Activation function
    public void Activate()
    {
        if(IsPossessed)
        {
            if (Reversible || (!Reversible && !ActivatedOnce))
            {
                Camera.main.GetComponent<RipplePostProcessor>().Ripple(PlayerJointPosition.transform.position);
                if(InteractionType == InteractionType.Flip)
                {
                    Object_Animator.SetTrigger("flip");
                }
                else
                {
                    Object_Animator.SetBool("isTranslating", true);
                }
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
        if (possess)
        {
            Camera.main.GetComponent<RipplePostProcessor>().Ripple(PlayerJointPosition.transform.position);
            audioManager.PlaySound(audioManager.Possess);
            if(IsEndObject)
            {
                audioManager.PlaySound(audioManager.EndOfLevel);
                FindObjectOfType<MainManager>().DisplayScore();
            }
        }
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