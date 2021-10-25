using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmShotDanger : MonoBehaviour, ActivateDanger
{
    public GameObject part = null;
    private ArmShotAttack aAtk = null;

    public void BecomeDangerous()
    {
        //Debug.Log("Arm shot dangerous");
        try {
            aAtk.canMove = true;
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    public void BecomeSafe()
    {
        //Debug.Log("Arm shot safe");
        try {
            aAtk.canMove = false;
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aAtk = part.GetComponent<ArmShotAttack>();
        BecomeSafe();
    }
}
