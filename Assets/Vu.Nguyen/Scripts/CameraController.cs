using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    public Text txt;
    public float scrollSpeed;
    private Vector3 drag;
    private Vector3 click; 
    private Vector3 vitribandau;

    float timeDetectTouch = 0.3f;
    bool flagNhan;

    //private Vector3 startPosition;

    void Start()
    {
        click = Vector3.zero;
        vitribandau = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timeDetectTouch -= Time.deltaTime;
            flagNhan = true;

            if (click == Vector3.zero)
            {
                click = Input.mousePosition;
                vitribandau = transform.position;
            }
            drag = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0))
        {
            flagNhan = false;
        }
        //khi không nhấn nữa và thời gian nhấn lớn hơn 0 và đã click thì coi như là click
        if (flagNhan == false && timeDetectTouch >= 0 && click != Vector3.zero)
        {
            Debug.Log("click");
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider == null)
                txt.text = "null";
            else
                txt.text = hit.collider.name;
            click = Vector3.zero;
        }
        //khi không nhấn nhưng thời gian nhấn giữ lâu
        if (flagNhan == false && timeDetectTouch < 0 && click != Vector3.zero)
        {
            timeDetectTouch = 0.3f;
            click = Vector3.zero;
        }
        if (click == Vector3.zero)
            return;
        if ((vitribandau.x + ((click.x - drag.x) * scrollSpeed) < 483f) && (vitribandau.x + ((click.x - drag.x) * scrollSpeed) > -435f))
            transform.position = new Vector3(vitribandau.x + ((click.x - drag.x) * scrollSpeed),
                -107, -411);
        //Hàng trước
    }
}
    