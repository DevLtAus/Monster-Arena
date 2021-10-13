using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private MovePlayer movPlayer;
    private bool aerial;
    private Camera cam;

    // (Lucas) Some things for camera bounds, will uncomment and implement them
    // (Lucas) once the arena has a background. Will give background a box collider
    // (Lucas) that will then be used to get min and max bounds to use to clamp the
    // (Lucas) camera position.
    [SerializeField] private BoxCollider2D arenaBounds;
    private Vector2 boundsMin;
    private Vector2 boundsMax;
    private float camY, camX;
    private float camOrthSize;
    private float camRatio;

    // (Lucas) How much the camera gets pushed around by movement and mouse position.
    // (Lucas) Seperate values since the movement will push first and, ideally, push
    // (Lucas) more than the mouse.
    // (Lucas) They are stored as Vector2 values to allow for influence to differ
    // (Lucas) between axes.
    [SerializeField] private Vector2 moveInfluence; // (Lucas) 2, 2
    [SerializeField] private Vector2 mouseInfluence; // (Lucas) 1.5, 1.5

    // (Lucas) Buffer regions for player speed. Move is the normal speed region
    // (Lucas) for walking and jumping, fast is to cover circumstances when the
    // (Lucas) camera may not be fast enough.
    [SerializeField] private Vector2 minMoveVel; // (Lucas) 3, 18
    [SerializeField] private Vector2 minFastVel; // (Lucas) 20, 35
    [SerializeField] private float fastMod; // (Lucas) 1.5

    // (Lucas) How fast the camera moves, the default camera offset, the actual
    // (Lucas) camera offset, how far away the camera can offset, the position
    // (Lucas) to move the camera to.
    private float camSpeed;
    [SerializeField] private float defaultCamSpeed; // (Lucas) 8
    [SerializeField] private float upSpeed; // (Lucas) 4
    [SerializeField] private float downSpeed; // (Lucas) 4
    private Vector3 camOffset;
    [SerializeField] private Vector3 camOffsetDefault; // (Lucas) 0, 3.7, -10
    private Vector3 targetPos;

    // (Lucas) Dealing with vertical movement. 0 is none, 1 is up, 2 is down.
    private int up = 0;

    // (Lucas) fetching mouse position and player position internally, may want to
    // (Lucas) tweak other scripts to be completely certain that values here don't
    // (Lucas) vary from those in other scripts. Using same method as in Weapon.cs
    // (Lucas) in the meantime.
    private Vector3 mousePos;
    private Vector3 playerPos;
    private float mpDistance;
    private Vector3 mpDir;
    private bool mRight;
    private bool mUp;
    //[SerializeField] private Vector2 mModifier; // (Lucas) 1, 1

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = playerTrans.position + camOffsetDefault;
        aerial = movPlayer.Aerial;

        arenaBounds = GameObject.Find("Background").GetComponent<BoxCollider2D>();

        // (Lucas) Stuff for camera bounds.
        boundsMin = new Vector2(arenaBounds.bounds.min.x, arenaBounds.bounds.min.y);
        boundsMax = new Vector2(arenaBounds.bounds.max.x, arenaBounds.bounds.max.y);
        camOrthSize = cam.orthographicSize;
        camRatio = (boundsMax.x + camOrthSize) / 2.0f;
    }

    // (Elliot) Set the Mouse's influence on the camera
    public void SetMouseInput(Vector3 mi)
    {
        mousePos = mi;
    }

    void FixedUpdate()
    {
        aerial = movPlayer.Aerial;

        // (Lucas) Movement's influence on the camera
        Vector2 playerVel = playerBody.velocity;
        if (Mathf.Abs(playerVel.x) < minFastVel.x) {
            if (Mathf.Abs(playerVel.x) < minMoveVel.x) {
                // (Lucas) Stopped
                camOffset = new Vector2(camOffsetDefault.x, 0);
            }
            else {
                // (Lucas) Normal
                camOffset = new Vector2(moveInfluence.x, 0);
            }
        }
        else {
            // (Lucas) Fast
            camOffset = new Vector2(moveInfluence.x * fastMod, 0);
        }

        switch(aerial) {
            case true:
                // (Lucas) In the air
                camOffset.y = moveInfluence.y;
                if (playerVel.y <= 0) {
                    up = 2;
                }
                else {
                    up = 1;
                }

                if (Mathf.Abs(playerVel.y) > minMoveVel.y) {
                    switch(up) {
                        case 1:
                            // (Lucas) Up
                            camSpeed = upSpeed;
                            break;
                        case 2:
                            // (Lucas) Down
                            camSpeed = downSpeed;
                            break;
                    }
                    if (Mathf.Abs(playerVel.y) > minFastVel.y) {
                        camSpeed *= fastMod;
                    }
                }
                break;
            case false:
                // (Lucas) On thr ground
                camOffset.y = camOffsetDefault.y;
                up = 0;
                camSpeed = defaultCamSpeed;
                break;
        }

        if (playerVel.x < 0) {
            camOffset.x *= -1;
        }

        // (Lucas) Mouse's influence on the camera
        //mousePos = Input.mousePosition;

        // (Lucas) Get mouse position relative to player position
        playerPos = Camera.main.WorldToScreenPoint(playerTrans.position);
        // mousePos.x = (mousePos.x - playerPos.x);
        // mousePos.y = (mousePos.y - playerPos.y);
        
        Vector3 target = mousePos - playerPos;
        mpDistance = target.magnitude;
        mpDir = target / mpDistance;
        // Debug.Log(target);
        // Debug.Log(mpDistance);
        // Debug.Log(mpDir);

        // (Lucas) Setting targetPos. Will comment out when camera bounds is in.
        //targetPos = new Vector3(playerTrans.position.x + camOffset.x + (mpDir.x * mouseInfluence.x),
            //playerTrans.position.y + camOffset.y + (mpDir.y * mouseInfluence.y), this.transform.position.z);

        // (Lucas) Clamping to camera bounds
        camY = Mathf.Clamp(playerTrans.position.y + camOffset.y + (mpDir.y * mouseInfluence.y),
            boundsMin.y + camOrthSize, boundsMax.y - camOrthSize);
        camX = Mathf.Clamp(playerTrans.position.x + camOffset.x + (mpDir.x * mouseInfluence.x),
            boundsMin.x + camOrthSize, boundsMax.x - camOrthSize);
        targetPos = new Vector3(camX, camY, this.transform.position.z);

        
        Vector3 lerpPos = Vector3.Lerp(transform.position, targetPos, camSpeed * Time.deltaTime);
        
        // (Lucas) Vertical movement
        /*switch(up) {
            case 0:
                // (Lucas) none
                break;
            case 1:
                // (Lucas) up
                break;
            case 2:
                // (Lucas) down
                break;
            default:
                break;
        }*/

        // (Lucas) Move
        transform.position = lerpPos;
    }
}
