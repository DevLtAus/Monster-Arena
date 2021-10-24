using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedAttack : MonoBehaviour, ActivateDanger
{

    public GameObject part = null;
    private Animator anim = null;

    public void BecomeDangerous()
    {
        anim.SetBool("waiting", false);
    }

    public void BecomeSafe()
    {
        anim.SetBool("waiting", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = part.GetComponent<Animator>();
        BecomeSafe();
    }
}
