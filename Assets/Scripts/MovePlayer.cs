using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody2D body;
    private Transform trans;
    [SerializeField] private float horizontalInput = 0f;

    // (Lucas) Buffer timers for if the player is trying to jump while not on the ground.
    private int jumpBufferTimer = 0; // (Lucas) Trying to jump right before landing.
    private int fallBufferTimer = 0; // (Lucas) Trying to jump right after running off a platform.

    // (Lucas) Serialising these fields so they can still be accessed in the inspector.
    // (Lucas) I've added values that I've found work decent.
    [SerializeField] private float accel; // (Lucas) 3
    [SerializeField] private float maxSpeed; // (Lucas) 15
    [SerializeField] private float maxRocketSpeed; // (Lucas)
    [SerializeField] private float jumpSpeed; // (Lucas) 23
    [SerializeField] private int jumpBuffer; // (Lucas) 7
    [SerializeField] private int fallBuffer; // (Lucas) 2

    // (Lucas) Enabling different behaviours in the air and on the ground.
    // (Lucas) This is just for making the movement feel good.
    [SerializeField] private float apexGrav; // (Lucas) 3
    [SerializeField] private float riseGrav; // (Lucas) 4
    [SerializeField] private float fallGrav; // (Lucas) 13
    [SerializeField] private float groundGrav; // (Lucas) 3
    [SerializeField] private float airDrag; // (Lucas) 1
    [SerializeField] private float groundDrag; // (Lucas) 10
    [SerializeField] private float apexBufferPos; // (Lucas) 1
    [SerializeField] private float apexBufferNeg; // (Lucas) -1

    // (Lucas) Booleans for being in the air.
    private bool buffering = false;
    private bool jumped = false;
    private bool canAirJump = false;

    // (Lucas) Whether or not the player is in the air. Leaving get and set commented out since they're not likely to be useful but there's a chance.
    [SerializeField] private bool aerial = false;
    public bool Aerial
    {
        get { return aerial; }
        set { aerial = value; }
    }

    // (Lucas) Rocket jumping stuff.
    // (Lucas) Making pushed public since it is entirely for detecting if the player is being pushed by something else.
    public bool pushed = false;
    private bool pushing = false;


    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        trans = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // (Lucas) Get horizontal input and store it.
        horizontalInput = Input.GetAxis("Horizontal");
        // (Lucas) Check if the player wants to jump.
        if (Input.GetButtonDown("Jump")) {
            switch(aerial) {
                case true:
                    switch(canAirJump) {
                        case false:
                            // (Lucas) In the air and can't jump but can start buffering the next jump in case the player was a couple frames early.
                            jumpBufferTimer = jumpBuffer;
                            buffering = true;
                            break;
                        case true:
                            switch(jumped) {
                                case true:
                                    break;
                                case false:
                                    jumped = true;
                                    break;
                            }
                            break;
                    }
                    break;
                case false:
                    // (Lucas) On the ground, can jump.
                    switch(jumped) {
                        case true:
                            break;
                        case false:
                            jumped = true;
                            break;
                    }
                    break;
            }
        }
    }

    // (Lucas) Physics calculations are called here.
    void FixedUpdate()
    {
        // (Lucas) Make the jumpBufferTimer tick down.
        if (jumpBufferTimer > 0) {
            jumpBufferTimer -= 1;
        }
        else {
            switch(buffering) {
                case true:
                    buffering = false;
                    break;
                case false:
                    break;
            }
        }

        // (Lucas) Make the fallBufferTimer tick down.
        if (fallBufferTimer > 0) {
            fallBufferTimer -= 1;
        }
        else {
            switch(canAirJump) {
                case true:
                    canAirJump = false;
                    break;
                case false:
                    break;
            }
        }

        

        // (Lucas) Check if the player is in the air then set gravity and drag accordingly.
        switch(aerial) {
            case true:
                body.drag = airDrag;
                if (apexBufferNeg <= body.velocity.y && body.velocity.y <= apexBufferPos) {
                    body.gravityScale = apexGrav;
                }
                else if (body.velocity.y < apexBufferNeg || (body.velocity.y > apexBufferPos && !Input.GetButton("Jump"))) {
                    body.gravityScale = fallGrav;
                }
                else {
                    body.gravityScale = riseGrav;
                }
                break;
            case false:
                switch(pushing) {
                    case true:
                        pushing = false;
                        pushed = false;
                        break;
                    case false:
                        break;
                }
                body.gravityScale = groundGrav;
                body.drag = groundDrag;
                break;
        }

        // (Lucas) Check if the player jumped.
        // (Lucas) This doesn't really look all that nice. Too bad!
        switch(jumped) {
            case true:
                Jump();
                break;
            case false:
                switch(aerial) {
                    case true:
                        // (Lucas) Do nothing.
                        break;
                    case false:
                        switch(buffering) {
                            case true:
                                // (Lucas) The player landed while a jump was buffering.
                                Jump();
                                break;
                            case false:
                                // (Lucas) Do nothing.
                                break;
                        }
                        break;
                }
                break;
        }

        // (Lucas) Move the player.
        body.AddForce(horizontalInput * accel * trans.right, ForceMode2D.Impulse);
        // (Lucas) Check if the player is being pushed. If they aren't, cap their max speed.
        switch(pushed) {
            case true:
                // (Lucas) Rocket jumping. Player can move faster horizontally
                //pushing = true;
                if (Mathf.Abs(body.velocity.x) > maxRocketSpeed)
                {
                    body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxRocketSpeed, body.velocity.y);
                }
                break;
            case false:
                if (Mathf.Abs(body.velocity.x) > maxSpeed)
                {
                    body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
                }
                break;
        }
    }

    // (Lucas) Check if the player is on the ground.
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") {
            aerial = false;
            pushed = false;
            fallBufferTimer = 0;
        }
    }

    // (Lucas) Check if the player has left the ground. This accounts for walking straight off the edge of a platform.
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") {
            aerial = true;
            fallBufferTimer = fallBuffer;
            canAirJump = true;
        }
    }

    // (Lucas) Make the player jump.
    private void Jump()
    {
        body.drag = airDrag;
        body.gravityScale = riseGrav;
        body.AddForce(trans.up * jumpSpeed, ForceMode2D.Impulse);
        jumped = false;
        buffering = false;
        fallBufferTimer = 0;
        canAirJump = false;
    }
}
