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
    public RaycastHit2D[] hits;
    private int hitCount;
    //public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        filter = new ContactFilter2D();
        filter.useTriggers = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //RaycastHit2D hit = 
        hits = new RaycastHit2D[20];
        hitCount = Physics2D.Raycast(transform.position, newDirection, filter, hits);
        RaycastHit2D hit = hits[1];
        if (hit.collider != null)
        {
            hitPos = hit.transform.position;
            //distance = Mathf.Abs(hit.point.y - transform.position.y);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            distance = Vector2.Distance(hit.point, myPos);
        }
        Debug.DrawRay(transform.position, newDirection * targetDistance, Color.green);
        Debug.DrawRay(transform.position, newDirection * distance, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
