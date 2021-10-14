using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // (Lucas) The relative monster part.
    public GameObject part = null;

    // (Lucas) The monster part's animator, if applicable.
    // (Lucas) I could probably do a way more elegant solution using inheritance,
    // (Lucas) but that's overkill for this.
    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        anim = part.GetComponent<Animator>();
        if (anim != null) {
            anim.SetBool("waiting", true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && anim != null) {
            anim.SetBool("waiting", false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && anim != null) {
            anim.SetBool("waiting", true);
        }
    }
}
