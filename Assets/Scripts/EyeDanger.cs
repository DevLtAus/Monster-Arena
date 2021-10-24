using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDanger : MonoBehaviour, ActivateDanger
{
    public GameObject part = null;
    private EyeAttack eAtk = null;

    public void BecomeDangerous()
    {
        eAtk.attackAllowed = true;
    }

    public void BecomeSafe()
    {
        eAtk.attackAllowed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        eAtk = part.GetComponent<EyeAttack>();
        BecomeSafe();
    }
}
