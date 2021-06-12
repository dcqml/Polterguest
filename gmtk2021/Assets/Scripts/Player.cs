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

    // Start is called before the first frame update
    void Start()
    {
        CurrentlyPossessedObject = StartObject.GetComponent<Object>();
        CurrentlyPossessedObject.SetPossess(true);
        transform.position = CurrentlyPossessedObject.PlayerJointPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CurrentlyPossessedObject.PlayerJointPosition.transform.position;
        var CircleSize = Circle.transform.GetComponent<SpriteRenderer>().size;
        Circle.transform.localScale = new Vector3(PossessionRadius / CircleSize.x, PossessionRadius / CircleSize.y, 1);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastClickTime < catchTime)
            {
                print("Double click");
                var colliders = Physics2D.OverlapCircleAll(transform.position, PossessionRadius / 2.0f).ToList();
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
                if(colliders.Find(x => x == hit.collider))
                {
                    Object obj = hit.collider.gameObject.transform.parent.GetComponent<Object>();
                    if(obj != null)
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
        
        if(obj == CurrentlyPossessedObject)
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
}
