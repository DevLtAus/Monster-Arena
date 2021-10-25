using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShotAttack : MonoBehaviour
{
    // (Lucas) Are we allowed to move/attack?
    public bool canMove = false;

    // (Lucas) Are we aiming? Cannot move if so.
    private bool aiming = false;

    // (Lucas) What we are shooting at
    public Transform target;

    // (Lucas) How fast the arm rotates
    public float speed = 1;

    // (Lucas) Maximum and minimum rotations
    public float maxRotation;
    public float minRotation;

    // (Lucas) Where to rotate to
    public Vector3 defaultRotation = Vector3.zero;
    private Vector3 targetRotation;
    private Vector3 currentRotation;
    private Vector3 targetDirection;
    public Vector3 defaultDirection;
    private Quaternion tempRot;
    //private float tempRotZ = 0;

    // (Lucas) How far we can shoot
    public float range = 1;

    // (Lucas) What we are shooting from
    public GameObject shotSource;

    // (Lucas) What we are shooting
    public LaunchedSpike spikeFab;
    private float angle = 0;

    // (Lucas) Speed of the spike
    public float spikeSpeed = 1;

    // (Lucas) Shooting cooldown and timer
    public float cooldown;
    private float cooldownTimer = 0;

    // (Lucas) Aiming time and timer
    public float aimTime;
    private float aimTimer = 0;

    // (Lucas) Raycast for aiming
    public RaycastHit2D[] hits;
    private int hitCount;
    private ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(defaultRotation);
        currentRotation = transform.localRotation.eulerAngles;
        filter = new ContactFilter2D();
        filter.useTriggers = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float singleStep = speed * Time.deltaTime;

        switch(canMove) {
            case true:
                //targetDirection = target.position - transform.position;
                switch(aiming) {
                    case false:
                        targetDirection = target.position - transform.position;
                        if(cooldownTimer > 0) {
                            cooldownTimer -= 1;
                        }
                        else {
                            // (Lucas) Are we pointed at the player?
                            // (Lucas) If so, aim.
                            hits = new RaycastHit2D[20];
                            hitCount = Physics2D.Raycast(shotSource.transform.position, shotSource.transform.up, filter, hits);
                            RaycastHit2D hit = hits[0];
                            if (hit.collider != null) {
                                if (hit.transform.gameObject.tag == "Player") {
                                    aimTimer = aimTime;
                                    aiming = true;
                                }
                            }
                            Debug.DrawRay(shotSource.transform.position, shotSource.transform.up * range, Color.red);
                        }
                        break;
                    case true:
                        if(aimTimer > 0) {
                            aimTimer -= 1;
                        }
                        else {
                            Fire();
                        }
                        break;
                }
                break;
            case false:
                targetDirection = defaultDirection;
                break;
        }

        // (Lucas) Rotate to point in the target direction, clamped.
        Vector3 newDirection = Vector3.RotateTowards(transform.up, targetDirection, singleStep, 0.0f);
        tempRot = Quaternion.LookRotation(Vector3.forward, newDirection);
        currentRotation = tempRot.eulerAngles;
        currentRotation.z = Mathf.Clamp(currentRotation.z, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    // (Lucas) Shoot the spike
    private void Fire()
    {
        // (Lucas) Put everything the spike needs together
        angle = currentRotation.z;
        LaunchedSpike spike = Instantiate(spikeFab);
        spike.transform.position = shotSource.transform.position;
        spike.angleProp = angle;
        spike.speedProp = spikeSpeed;

        // (Lucas) Begin cooldown
        aiming = false;
        cooldownTimer = cooldown;
    }
}
