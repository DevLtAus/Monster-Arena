using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    //(Elliot) Array of Weakspot game objects added from scene
    public GameObject[] weakSpots;
    //(Elliot) Collects game manager object and components
    private GameObject gm;
    private HealthManager hm;
    private float health = 0;

    PhaseManager pm;

    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.Find("Game Manager");
        hm = gm.GetComponent<HealthManager>();
        pm = this.GetComponent<PhaseManager>();
        //(Elliot) Tallies weak spot health and sets boss health
        for (int i = 0; i < weakSpots.Length; i++)
        {
            WeakSpot ws = weakSpots[i].GetComponent<WeakSpot>();
            health += ws.health;

            if (ws.canActivateBoss) {
                pm.Phase = 0;
            }
        }
    
        hm.SetBossHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //(Elliot) Checking if weak spots have been damaged
        float healthCheck = 0;
        foreach (GameObject i in weakSpots)
        {
            WeakSpot ws = i.GetComponent<WeakSpot>();
            healthCheck += ws.health;
        }
        
        //(Elliot) Sets new health as damage is done and calls to update Health Manager
        if (healthCheck != health)
        {
            pm.Phase = 1;
            hm.DamageBoss(health - healthCheck);
            health = healthCheck;   
        }

        //Debug.Log(health); //Uncomment to see how boss health changes as damage is done to the weak spots
    }

}
