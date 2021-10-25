using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeDanger : MonoBehaviour, ActivateDanger
{
    public GameObject part = null;
    private EyeAttack eAtk = null;

    public void BecomeDangerous()
    {
        try {
            eAtk.attackAllowed = true;
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    public void BecomeSafe()
    {
        try {
            eAtk.attackAllowed = false;
        }
        catch (NullReferenceException ex) {
            //Debug.Log("Monster part isn't active");
            Debug.Log(ex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        eAtk = part.GetComponent<EyeAttack>();
        BecomeSafe();
    }
}
