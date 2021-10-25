using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatedAttack : MonoBehaviour, ActivateDanger
{

    public GameObject part = null;
    private Animator anim = null;

    public void BecomeDangerous()
    {
        try {
            anim.SetBool("waiting", false);
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    public void BecomeSafe()
    {
        try {
            anim.SetBool("waiting", true);
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = part.GetComponent<Animator>();
        BecomeSafe();
    }
}
