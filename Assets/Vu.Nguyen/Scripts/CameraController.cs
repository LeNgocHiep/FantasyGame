using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    public float scrollSpeed;
    public Text text;

    

    //private Vector3 startPosition;

    void Start()
    {
        //startPosition = transform.position;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(transform.position.x - transform.position.x * Time.deltaTime * scrollSpeed, 0,0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(transform.position.x + transform.position.x * Time.deltaTime * scrollSpeed, 0, 0));
        }


        //foreach(Touch t in Input.touches)
        //{
        //    if (t.phase == TouchPhase.Began)
        //    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider == null)
                text.text = "null";
            else
                text.text = hit.collider.name;
        }
        //    }
        //}
    }


    
}
