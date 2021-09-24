using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject player;
    // (Lucas) Storing a reference to MovePlayer to tell the player they've been pushed by a rocket.
    private MovePlayer movePlayer;
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
    //(Elliot) public variables for radius of impact and force for which to affect repelling the player
    public float fieldOfImpact;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        //(Elliot) Locates 'Player' Game Component
        player = GameObject.Find("Player");
        movePlayer = player.GetComponent<MovePlayer>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        //(Elliot) Fire rocket in direction of self space
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < fieldOfImpact)
            {
                //(Elliot) Formula for calculating force to repel given how close the player is to the rocket
                //e.g if distance = basically nothing, the full force will be applied. If the distance is barely within the FOI, only a small fraction of the force will result.
                force -= distance / fieldOfImpact * force;

                Vector2 direction = player.transform.position - transform.position;
                //(Elliot) Applying the force to player
                player.GetComponent<Rigidbody2D>().AddForce(direction * force);
                // (Lucas) Tell the player they have been pushed.
                movePlayer.pushed = true;
            }

            if (col.gameObject.tag == "Weak")
            {
                WeakSpot ws = col.GetComponent<WeakSpot>();
                ws.Damage();
            }
            //(Elliot) Create explosion effect and Destroy rocket on impact with anything but the player
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //(Elliot) Open the prefab in Unity to see and set the field of impact 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
