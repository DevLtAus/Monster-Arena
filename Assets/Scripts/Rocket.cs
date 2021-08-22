using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Weapon weapon;
    //(Elliot) Collects angle and speed variables from Weapon script
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
        //(Elliot) Locates 'Weapon' Game Component
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // transform.Translate(Vector3.right * 3);
    }

    // Update is called once per frame
    void Update()
    {
        //(Elliot) Fire rocket in direction of self space
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    //(Elliot) Destroy rocket on impact with anything but the player
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
