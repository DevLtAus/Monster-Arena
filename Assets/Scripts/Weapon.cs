using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //(Elliot) Weapon rotation will be done by comparing the mouse position to the position of the object
    private Vector3 mousePos;
    private Transform trans;
    private Vector3 objectPos;
    private float angle;

    //(Elliot) Collect rocket prefab for instantiation
    public Rocket rocketPrefab;
    private float curCooldown;
    //(Elliot) Speed and cooldown of rockets are public variables
    public float speed;
    public float cooldown;
    
 
    // Start is called before the first frame update
    void Start()
    {
       trans = this.gameObject.GetComponent<Transform>();
       curCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curCooldown -= Time.deltaTime;
    }

    public void Aim(Vector3 mousePos)
    {
        //(Elliot) Collect mouse and object positions based on screen space
        objectPos = Camera.main.WorldToScreenPoint(trans.position);
        Vector3 target = mousePos - transform.position;
        //(Elliot) Converts comparison to degrees and rotates the object around the z axis
        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //(Elliot) Instantiates a rocket in the direction of the mouse cursor when the mouse is clicked
    public void Shoot()
    {
        if (curCooldown <= 0) {
            curCooldown = cooldown;
            Rocket rocket = Instantiate(rocketPrefab);
            rocket.transform.position = transform.position;
            rocket.angleProp = angle;
            rocket.speedProp = speed;
        }
    }
}
