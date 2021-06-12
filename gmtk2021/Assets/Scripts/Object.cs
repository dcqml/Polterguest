using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Initialization
    
    public bool IsEndObject = false;
    public bool IsPossessed { get { return Object_Animator.GetBool("isPossessed"); } }

    public bool CanPatrol = false;
    public bool Reversible = true;

    public GameObject PlayerJointPosition;

    private Player player { get { return FindObjectOfType<Player>(); } }

    private bool ActivatedOnce = false;
    private Animator Object_Animator;

    public List<Object> TriggerObjects; // Objects that can be triggered on this object activation

    // Activation function
    public void Activate()
    {
        if(IsPossessed)
        {
            if (Reversible || (!Reversible && !ActivatedOnce))
            {
                Object_Animator.SetTrigger("activate");
                foreach (var obj in TriggerObjects)
                {
                    obj.Trigger();
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
    }
    
    // Patrol function
    public void Patrol()
    {
        Object_Animator.SetBool("isPatrolling", !(Object_Animator.GetBool("isPatrolling")));
    }

    // Trigger function
    public void Trigger()
    {
        Object_Animator.SetTrigger("trigger");
    }

    // Start is called before the first frame update
    void Start()
    {
        Object_Animator = GetComponent<Animator>();
    }
}

