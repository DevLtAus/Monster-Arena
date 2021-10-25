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
    private float tempRotZ = 0;

    // (Lucas) How far we can shoot
    public float range = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(defaultRotation);
        currentRotation = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(aiming) {
            case false:
                float singleStep = speed * Time.deltaTime;

                switch(canMove) {
                    case true:
                    targetDirection = target.position - transform.position;
                    break;
                case false:
                    targetDirection = defaultDirection;
                    break;
                }

                Vector3 newDirection = Vector3.RotateTowards(transform.up, targetDirection, singleStep, 0.0f);
                tempRot = Quaternion.LookRotation(Vector3.forward, newDirection);
                currentRotation = tempRot.eulerAngles;
                currentRotation.z = Mathf.Clamp(currentRotation.z, minRotation, maxRotation);
                transform.localRotation = Quaternion.Euler(currentRotation);
                break;
            case true:
                break;
        }
    }
}
