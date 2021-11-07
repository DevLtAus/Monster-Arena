using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // (Lucas) Stuff for fading the clouds in
    private int curLevel = 0;
    public CloudLevel[] cloudLevels;
    private bool fadedIn = false;

    // (Lucas) Stuff for making the clouds damage the player
    private bool damaging = false;
    private PolygonCollider2D poly;
    private HealthManager healthy;
    public float damage = 1;
    public bool hurting = false;

    // (Lucas) Timer for delay before damaging the player
    public float hurtDelay = 1;
    private float hurtDelayTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthy = GameObject.Find("Game Manager").GetComponent<HealthManager>();
        poly = gameObject.GetComponent<PolygonCollider2D>();
        poly.enabled = false;
    }

    void Awake()
    {
        healthy = GameObject.Find("Game Manager").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (fadedIn) {
            case true:
                switch (damaging) {
                    case false:
                        poly.enabled = true;
                        damaging = true;
                        break;
                    case true:
                        break;
                }
                break;
            case false:
                switch (cloudLevels[curLevel].faded) {
                    case true:
                        if (curLevel < cloudLevels.Length-1) {
                            curLevel += 1;
                        }
                        else {
                            fadedIn = true;
                        }
                        break;
                    case false:
                        switch (cloudLevels[curLevel].fading) {
                            case true:
                                break;
                            case false:
                                StartCoroutine(cloudLevels[curLevel].Fade());
                                break;
                        }
                        break;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (hurting) {
            case true:
                healthy.DamagePlayer(damage);
                hurtDelayTimer = hurtDelay;
                break;
            case false:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            hurting = true;
            hurtDelayTimer = hurtDelay;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            hurting = false;
        }
    }
}
