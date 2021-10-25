using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedSpike : MonoBehaviour
{
    // (Lucas) Will the spike damage the player?
    //private bool harming = true;

    // (Lucas) Is the spike moving?
    private bool moving = true;

    // (Lucas) Angle and speed work just like in Rocket.cs, but use ArmShotAttack.cs
    private float angle;
    public float angleProp
    {
        get { return angle; }
        set { angle = value; }
    }
    private float speed;
    public float speedProp
    {
        get { return speed; }
        set { speed = value; }
    }

    // (Lucas) References for when the spike hits the arena.
    // (Lucas) Upon impact with the arena (Ground tag):
    // (Lucas) Stops being able to do damage
    // (Lucas) Stops moving (rigidybody2d becomes static)
    // (Lucas) Sprite fades out
    private SpriteRenderer sRend;
    private Rigidbody2D rigid;
    private PolygonCollider2D poly;
    public float fadeSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        sRend = gameObject.GetComponent<SpriteRenderer>();
        rigid  = gameObject.GetComponent<Rigidbody2D>();
        poly = gameObject.GetComponent<PolygonCollider2D>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        switch(moving) {
            case true:
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
                break;
            case false:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") {
            moving = false;
            Destroy(poly);
            rigid.bodyType = RigidbodyType2D.Static;
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        float curAlpha = sRend.color.a;
        Color temp = sRend.color;

        while (sRend.color.a > 0)
        {
            curAlpha -= 0.01f;
            temp.a = curAlpha;
            sRend.color = temp;
            yield return new WaitForSeconds(fadeSpeed);
        }
        Destroy(gameObject);
    }
}
