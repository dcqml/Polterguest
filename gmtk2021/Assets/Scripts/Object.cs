using UnityEngine;

public class Object : MonoBehaviour
{
    // Initialization
    private Animator Object_Animator;
    public bool isPossessed = false;
    public bool isEndObject = false;
    public Child_Object Possess_Pos;

    // Activation function
    void Activate()
    {
        Object_Animator.SetBool("isActivated", !(Object_Animator.GetBool("isActivated")));

        if (Object_Animator.GetBool("isActivated") == true)
        {

        }
        else
        {

        }
    }

    // Trigger function
    void Trigger()
    {
        Object_Animator.SetBool("isTriggered", !(Object_Animator.GetBool("isTriggered")));
    }

    // Start is called before the first frame update
    void Start()
    {
        Object_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

