using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator anim;
    private bool facingRight;
    private bool moving;
    private bool aerial;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        facingRight = anim.GetBool("facingRight");
        moving = anim.GetBool("moving");
        aerial = anim.GetBool("aerial");
    }

    public void SetFacingRight(bool b)
    {
        anim.SetBool("facingRight", b);
    }

    public void SetMoving(bool b)
    {
        anim.SetBool("moving", b);
    }

    public void SetAerial(bool b)
    {
        anim.SetBool("aerial", b);
    }

    void Update()
    {
        
    }
}
