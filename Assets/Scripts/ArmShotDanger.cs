using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShotDanger : MonoBehaviour, ActivateDanger
{
    public GameObject part = null;

    public void BecomeDangerous()
    {
        Debug.Log("Arm shot dangerous");
    }

    public void BecomeSafe()
    {
        Debug.Log("Arm shot safe");
    }

    // Start is called before the first frame update
    void Start()
    {
        BecomeSafe();
    }
}
