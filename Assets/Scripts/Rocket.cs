using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Weapon weapon;
    private float angle;
    public float angleProp
    {
        get
        {
            return angle;
        }
        set
        {
            angle = value;
        }
    }
    private float speed;
    public float speedProp
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
