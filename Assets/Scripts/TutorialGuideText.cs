using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuideText : MonoBehaviour
{
    public float fadeTime;
    public Text textToShow;
    public bool showing;

    void OnTriggerEnter2D(Collider2D col)
    {
        // (Lucas) Show the text.
        if (col.gameObject.tag == "Player") {
            //Debug.Log("Showing text");
            showing = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // (Lucas) Hide the text.
        if (col.gameObject.tag == "Player") {
            //Debug.Log("Hiding text");
            showing = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textToShow.CrossFadeAlpha(0, 0, false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(showing) {
            case true:
                textToShow.CrossFadeAlpha(1, fadeTime, false);
                break;
            case false:
                textToShow.CrossFadeAlpha(0, fadeTime, false);
                break;
        }

    }
}
