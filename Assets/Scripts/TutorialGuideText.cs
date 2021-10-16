using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuideText : MonoBehaviour
{
    public float fadeTime;
    //private float fadeTimer;

    //public BoxCollider2D showZone;
    public Text textToShow;
    public bool showing;

    void OnTriggerEnter2D(Collider2D col)
    {
        // (Lucas) Show the text.
        if (col.gameObject.tag == "Player") {
            //Debug.Log("Showing text");
            //StartCoroutine(FadeTextIn());
            showing = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // (Lucas) Hide the text.
        if (col.gameObject.tag == "Player") {
            //Debug.Log("Hiding text");
            //StartCoroutine(FadeTextOut());
            showing = false;
        }
    }

    /*public IEnumerator FadeTextIn()
    {
        while (textToShow.color.a < 1)
        {
            Debug.Log("Fading in...");
            //textToShow.color = new Color(textToShow.color.r, textToShow.color.g, textToShow.color.b, textToShow.color.a + (Time.deltaTime / fadeTime));
            textToShow.CrossFadeAlpha(1, fadeTime, false);
            yield return null;
        }
        //yield return null;
    }*/

    /*public IEnumerator FadeTextOut()
    {
        while (textToShow.color.a < 0)
        {
            Debug.Log("Fading out...");
            //textToShow.color = new Color(textToShow.color.r, textToShow.color.g, textToShow.color.b, textToShow.color.a - (Time.deltaTime / fadeTime));
            textToShow.CrossFadeAlpha(0, fadeTime, false);
            yield return null;
        }
        //yield return null;
    }*/
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
