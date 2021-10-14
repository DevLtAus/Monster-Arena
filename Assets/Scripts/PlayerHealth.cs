using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameObject gManager;
    private HealthManager hManager;
    // Start is called before the first frame update
    void Start()
    {
        gManager = GameObject.Find("Game Manager");
        hManager = gManager.GetComponent<HealthManager>();
        hManager.SetPlayerHealth(hManager.playerMaxHealth);
    }
}
