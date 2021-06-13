using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float lastClickTime;
    public float catchTime = 0.25f;

    public Object CurrentlyPossessedObject;
    public float PossessionRadius = 2;

    public GameObject StartObject;
    public GameObject Circle;

    public float Speed;

    private void Awake()
    {
        CurrentlyPossessedObject = StartObject.GetComponent<Object>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentlyPossessedObject.SetPossess(true);
        transform.position = CurrentlyPossessedObject.PlayerJointPosition.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, CurrentlyPossessedObject.PlayerJointPosition.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrentlyPossessedObject.PlayerJointPosition.transform.position, Speed);
        }
        else
        {
            transform.position = CurrentlyPossessedObject.PlayerJointPosition.transform.position;
        }
        Circle.transform.localScale = new Vector3(PossessionRadius, PossessionRadius, 1);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastClickTime < catchTime)
            {
                print("Double click");
                var colliders = Physics2D.OverlapCircleAll(transform.position, PossessionRadius).ToList();
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mouseRay.origin, mouseRay.direction);
                if(hits.Length > 0)
                {
                    Object obj;
                    float distMin = float.MaxValue;
                    float dist;
                    Object objMin = null;
                    foreach (var rchit in hits)
                    {
                        obj = rchit.collider.gameObject.transform.parent.GetComponent<Object>();
                        if (obj != null)
                        {
                            dist = Vector2.Distance(mousePos, obj.PlayerJointPosition.transform.position);
                            if(dist < distMin)
                            {
                                distMin = dist;
                                objMin = obj;
                            }
                        }
                    }
                    if(objMin != null) DoSmth(objMin);
                }
            }
            else
            {
                print("nothing here");
            }
            lastClickTime = Time.time;
        }
    }

    void UpdateOld()
    {
        if (Vector2.Distance(transform.position, CurrentlyPossessedObject.PlayerJointPosition.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrentlyPossessedObject.PlayerJointPosition.transform.position, Speed);
        }
        else
        {
            transform.position = CurrentlyPossessedObject.PlayerJointPosition.transform.position;
        }
        Circle.transform.localScale = new Vector3(PossessionRadius, PossessionRadius, 1);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastClickTime < catchTime)
            {
                print("Double click");
                var colliders = Physics2D.OverlapCircleAll(transform.position, PossessionRadius).ToList();
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
                if (colliders.Find(x => x == hit.collider))
                {
                    Object obj = hit.collider.gameObject.transform.parent.GetComponent<Object>();
                    if (obj != null)
                    {
                        DoSmth(obj);
                    }
                }
            }
            else
            {
                print("nothing here");
            }
            lastClickTime = Time.time;
        }
    }

    private void DoSmth(Object obj)
    {
        if(!obj.CanPatrol)
        {
            if (obj == CurrentlyPossessedObject)
            {
                print($"interaction avec {obj.name}");
                CurrentlyPossessedObject.Activate();
            }
            else
            {
                CurrentlyPossessedObject.SetPossess(false);
                print($"possession de {obj.name}");
                CurrentlyPossessedObject = obj;
                CurrentlyPossessedObject.SetPossess(true);
            }
        }
        else
        {
            if(obj.GetComponent<Animator>().GetBool("isIdlePostTrigger"))
            {
                CurrentlyPossessedObject.SetPossess(false);
                print($"possession du patrouilleur {obj.name}");
                CurrentlyPossessedObject = obj;
                Camera.main.GetComponent<RipplePostProcessor>().Ripple(obj.PlayerJointPosition.transform.position);
            }
        }
    }
}
