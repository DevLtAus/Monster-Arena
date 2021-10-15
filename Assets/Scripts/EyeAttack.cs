using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAttack : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public float range = 1;
    public float distance = 0;
    private float targetDistance = 0;
    public Vector3 hitPos;
    private ContactFilter2D filter;

    // (Lucas) Raycast for aiming.
    private RaycastHit2D[] hits;
    private int hitCount;

    // (Lucas) Timer for attack delay.
    private float attackDelayTimer = 0;
    public float attackDelay;
    public bool attacking = false;

    // (Lucas) Timer for attack cooldown.
    private float cooldownTimer = 0;
    public float cooldown;
    public bool coolingDown = false;

    // (Lucas) Are we allowed to attack?
    public bool attackAllowed = false;

    //public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        filter = new ContactFilter2D();
        filter.useTriggers = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // (Lucas) Where to aim and how far away the player is.
        targetDistance = Vector3.Distance(target.position, transform.position);
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        hits = new RaycastHit2D[20];
        hitCount = Physics2D.Raycast(transform.position, newDirection, filter, hits);
        RaycastHit2D hit = hits[1];
        if (hit.collider != null) {
            hitPos = hit.transform.position;
            //distance = Mathf.Abs(hit.point.y - transform.position.y);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            distance = Vector2.Distance(hit.point, myPos);
            if (hit.transform.gameObject.tag == "Player" && !coolingDown && attackAllowed && !attacking) {
                attacking = true;
                attackDelayTimer = attackDelay;
            }
        }
        Debug.DrawRay(transform.position, newDirection * targetDistance, Color.green);
        Debug.DrawRay(transform.position, newDirection * distance, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDirection);

        switch(attacking) {
            case false:
                transform.rotation = Quaternion.LookRotation(newDirection);
                switch(coolingDown) {
                    case true:
                        if (cooldownTimer > 0) {
                            cooldownTimer -= 1;
                        }
                        else {
                            coolingDown = false;
                        }
                        break;
                    case false:
                        break;
                }
                break;
            case true:
                switch(attackAllowed) {
                    case true:
                        //Attack();
                        if (attackDelayTimer > 0) {
                            attackDelayTimer -= 1;
                        }
                        else {
                            Attack();
                        }
                        break;
                    case false:
                        break;
                }
                break;
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking the player");
        attacking = false;
        Cooldown();
    }

    private void Cooldown()
    {
        cooldownTimer = cooldown;
        coolingDown = true;
    }
}
