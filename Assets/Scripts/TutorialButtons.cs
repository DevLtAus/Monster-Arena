using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    private SpriteRenderer[] sRend;
    public float fadeSpeed = 0.05f;
    public bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        sRend = GetComponentsInChildren<SpriteRenderer>();
        Color temp = sRend[0].color;
        temp.a = 0.0f;
        foreach (SpriteRenderer sr in sRend)
        {
            sr.color = temp;
        }
    }

    public IEnumerator FadeIn()
    {
        float curAlpha = sRend[0].color.a;
        Color temp = sRend[0].color;

        while (sRend[0].color.a < 1 && show == true)
        {
            curAlpha += 0.01f;
            temp.a = curAlpha;
            foreach (SpriteRenderer sr in sRend)
            {
                sr.color = temp;
            }
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    public IEnumerator FadeOut()
    {
        float curAlpha = sRend[0].color.a;
        Color temp = sRend[0].color;

        while (sRend[0].color.a > 0 && show == false)
        {
            curAlpha -= 0.01f;
            temp.a = curAlpha;
            foreach (SpriteRenderer sr in sRend)
            {
                sr.color = temp;
            }
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
}
