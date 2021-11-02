using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLevel : MonoBehaviour
{
    private SpriteRenderer[] sRend;
    public float fadeSpeed = 0.05f;
    public bool fading = false;
    public bool faded = false;

    // Start is called before the first frame update
    void Start()
    {
        sRend = GetComponentsInChildren<SpriteRenderer>();
        Color temp = sRend[0].color;
        temp.a = 0;
        foreach (SpriteRenderer sr in sRend)
        {
            sr.color = temp;
        }
    }

    public IEnumerator Fade()
    {
        fading = true;
        float curAlpha = sRend[0].color.a;
        Color temp = sRend[0].color;

        while (sRend[0].color.a < 1)
        {
            curAlpha += 0.01f;
            temp.a = curAlpha;
            foreach (SpriteRenderer sr in sRend)
            {
                sr.color = temp;
            }
            yield return new WaitForSeconds(fadeSpeed);
        }
        faded = true;
        fading = false;
    }
}
