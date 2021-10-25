using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShotDanger : MonoBehaviour, ActivateDanger
{
    public GameObject part = null;
    private ArmShotAttack aAtk = null;

    public void BecomeDangerous()
    {
        //Debug.Log("Arm shot dangerous");
        aAtk.canMove = true;
    }

    public void BecomeSafe()
    {
        //Debug.Log("Arm shot safe");
        aAtk.canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        aAtk = part.GetComponent<ArmShotAttack>();
        BecomeSafe();
    }
}
