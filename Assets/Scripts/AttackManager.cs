using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // (Lucas) Does this attack use an animation?
    public bool animated;

    // (Lucas) Does this attack use an eye attack script?
    public bool eyeAttack;

    // (Lucas) The relative monster part.
    public GameObject part = null;

    // (Lucas) The monster part's animator, if applicable.
    // (Lucas) I could probably do a way more elegant solution using inheritance,
    // (Lucas) but that's overkill for this.
    private Animator anim = null;
    private EyeAttack eAtk = null;

    // Start is called before the first frame update
    void Start()
    {
        switch(animated) {
            case true:
                anim = part.GetComponent<Animator>();
                if (anim != null) {
                    anim.SetBool("waiting", true);
                }
                break;
            case false:
                switch(eyeAttack) {
                    case true:
                        eAtk = part.GetComponent<EyeAttack>();
                        if (eAtk != null) {
                            eAtk.attackAllowed = false;
                        }
                        break;
                    case false:
                        break;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(animated) {
            case true:
                if (col.gameObject.tag == "Player" && anim != null) {
                    anim.SetBool("waiting", false);
                }
                break;
            case false:
                switch(eyeAttack) {
                    case true:
                        if (col.gameObject.tag == "Player" && eAtk != null) {
                            eAtk.attackAllowed = true;
                        }
                        break;
                    case false:
                        break;
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch(animated) {
            case true:
                if (col.gameObject.tag == "Player" && anim != null) {
                    anim.SetBool("waiting", true);
                }
                break;
            case false:
                switch(eyeAttack) {
                    case true:
                        if (col.gameObject.tag == "Player" && eAtk != null) {
                            eAtk.attackAllowed = false;
                        }
                        break;
                    case false:
                        break;
                }
                break;
        }
    }
}
