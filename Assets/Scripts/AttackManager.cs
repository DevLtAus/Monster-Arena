using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // (Lucas) Which part we are activating
    public GameObject part = null;

    // (Lucas) The activation script for part
    private ActivateDanger aDanger = null;

    // Start is called before the first frame update
    void Start()
    {
        aDanger = part.GetComponent<ActivateDanger>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // (Lucas) If the player entered, activate the part.
        if (col.gameObject.tag == "Player") {
            aDanger.BecomeDangerous();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // (Lucas) If the player entered, activate the part.
        if (col.gameObject.tag == "Player") {
            aDanger.BecomeDangerous();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // (Lucas) If the player left, deactivate the part.
        if (col.gameObject.tag == "Player") {
            aDanger.BecomeSafe();
        }
    }
}
