using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private float playerX;
    //private float playerY;
    private float myX;
    private bool playerAerial;
    private bool pushLeft;
    //private bool moreHorizForce = false;
    private HealthManager healthy;
    public GameObject player;
    private Rigidbody2D playerRigid;
    private MovePlayer movP;
    private Vector2 push;
    public int damage = 1;
    public float horizGroundForce = 7000;
    public float horizAirForce = 6000;
    public float vertForce = 900;

    void Start()
    {
        healthy = GameObject.Find("Game Manager").GetComponent<HealthManager>();
        player = GameObject.Find("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        movP = player.GetComponent<MovePlayer>();
        myX = gameObject.transform.position.x;
    }

    void Awake()
    {
        healthy = GameObject.Find("Game Manager").GetComponent<HealthManager>();
        player = GameObject.Find("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        movP = player.GetComponent<MovePlayer>();
        myX = gameObject.transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            Hurt();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            Hurt();
        }
    }

    private void Hurt()
    {
        playerAerial = movP.Aerial;
        playerX = player.transform.position.x;
        //playerY = player.transform.position.y;
        if (playerX <= myX) {
            pushLeft = true;
        }
        else {
            pushLeft = false;
        }

        // (Lucas) Set the player's velocity to zero here to improve reliability.
        playerRigid.velocity = Vector3.zero;

        switch(playerAerial) {
            case true:
                switch(pushLeft) {
                    case true:
                        push = new Vector2(horizAirForce * -1, vertForce);
                        break;
                    case false:
                        push = new Vector2(horizAirForce, vertForce);
                        break;
                }
                break;
            case false:
                switch(pushLeft) {
                    case true:
                        push = new Vector2(horizGroundForce * -1, vertForce);
                        break;
                    case false:
                        push = new Vector2(horizGroundForce, vertForce);
                        break;
                }
                break;
        }

        playerRigid.AddForce(push);

        //Debug.Log("Hurting player");

        healthy.DamagePlayer(damage);
    }
}
