using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    //private int levels;
    private int curLevel = 0;
    public CloudLevel[] cloudLevels;
    private bool fadedIn = false;
    private bool damaging = false;
    private PolygonCollider2D poly;
    // Start is called before the first frame update
    void Start()
    {
        poly = gameObject.GetComponent<PolygonCollider2D>();
        poly.enabled = false;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (fadedIn) {
            case true:
                switch (damaging) {
                    case false:
                        poly.enabled = true;
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
                        //StartCoroutine(cloudLevels[curLevel].Fade());
                        break;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            Debug.Log("Player is in danger");
        }
    }
}
