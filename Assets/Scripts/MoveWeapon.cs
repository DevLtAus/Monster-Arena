using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeapon : MonoBehaviour
{
    //(Elliot) Weapon rotation will be done by comparing the mouse position to the position of the object
    private Vector3 mousePos;
    private Transform trans;
    private Vector3 objectPos;
    private float angle;
 
    // Start is called before the first frame update
    void Start()
    {
       trans = this.gameObject.GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //(Elliot) Collect mouse and object positions based on screen space
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(trans.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        //(Elliot) Converts comparison to degrees and rotates the object around the z axis
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
