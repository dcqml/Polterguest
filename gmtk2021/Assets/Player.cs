using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float lastClickTime;
    public float catchTime = 0.25f;

    public GameObject CurrentlyPossessedObject;
    public float PossessionRadius = 2;

    public GameObject Circle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                    DoSmth(hit.collider.gameObject);
                }
            }
            else
            {
                //normal click
            }
            lastClickTime = Time.time;
        }
    }

    private void DoSmth(GameObject obj)
    {
        
        if(obj == CurrentlyPossessedObject)
        {
            print($"interaction avec {obj.name}");
            //Interact();
        }
        else
        {
            print($"possession de {obj.name}");
            CurrentlyPossessedObject = obj;
            transform.position = CurrentlyPossessedObject.transform.position;
        }
    }
}
