using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Weapon weapon;
    private GameObject player;
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
        player = GameObject.Find("Player");
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
            if (Vector2.Distance(transform.position, player.transform.position) < 3)
            {
                Vector2 direction = player.transform.position - transform.position;
                Debug.Log("yes");
                player.GetComponent<Rigidbody2D>().AddForce(direction);
            }
            Destroy(gameObject);
        }
    }
}
