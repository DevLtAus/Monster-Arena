using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBeam : MonoBehaviour
{
    /*public GameObject beamStart;
    public GameObject beamEnd;
    private LineRenderer beam;

    // Start is called before the first frame update
    void Start()
    {
        beam = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> pos = new List<Vector3>();
        pos.Add(beamStart.transform.position);
        pos.Add(beamEnd.transform.position);
        beam.startWidth = 0.1f;
        beam.endWidth = 0.2f;
        beam.SetPositions(pos.ToArray());
        beam.useWorldSpace = true;
    }*/

    public GameObject rayStart;
    public GameObject rayTarget;
    public float range;
    private Vector2 direction;
    private Ray ray;
    private RaycastHit hitData;
    private Vector3 hitPos;
    public LayerMask layerMask;

    // (Lucas) Draw the ray
    private LineRenderer beam;
    private GameObject beamStart;
    private GameObject beamEnd;
    private List<Vector3> pos;

    // Start is called before the first frame update
    void Start()
    {
        beam = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (rayTarget.transform.position - rayStart.transform.position).normalized;
        ray = new Ray(rayStart.transform.position, direction);
        if (Physics.Raycast(ray, out hitData, range, layerMask)) {
            hitPos = hitData.point;
            Debug.Log(hitPos);

            //Debug.DrawRay(ray.origin, ray.direction * range);
            Debug.DrawLine(ray.origin, hitData.point);
            //Debug.Log("Ray hit: " + hitData.transform.gameObject.name);

            pos = new List<Vector3>();
            pos.Add(rayStart.transform.position);
            pos.Add(hitPos);
            beam.startWidth = 0.1f;
            beam.endWidth = 0.2f;
            beam.SetPositions(pos.ToArray());
            beam.useWorldSpace = true;
        }
        /*
        //Debug.DrawRay(ray.origin, ray.direction * range);
        Debug.DrawLine(ray.origin, hitData.point);
        //Debug.Log("Ray hit: " + hitData.transform.gameObject.name);
        
        pos = new List<Vector3>();
        pos.Add(rayStart.transform.position);
        pos.Add(hitPos);
        beam.startWidth = 0.1f;
        beam.endWidth = 0.2f;
        beam.SetPositions(pos.ToArray());
        beam.useWorldSpace = true;*/
        
    }
}
