using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public string bossName;
    //(Elliot) Array of Weakspot game objects added from scene
    public GameObject[] weakSpots;
    //(Elliot) Collects game manager object and components
    private GameObject gm;
    private HealthManager hm;
    private float health = 0;
    private bool activatable = false;
    private bool activated = true;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager");
        hm = gm.GetComponent<HealthManager>();
        //(Elliot) Tallies weak spot health and sets boss health
        for (int i = 0; i < weakSpots.Length; i++)
        {
            WeakSpot ws = weakSpots[i].GetComponent<WeakSpot>();
            health += ws.health;

            if (ws.canActivateBoss) {
                activatable = true;
                activated = false;
            }
        }

        //(Elliot) Deactivates all weakspots at the start except the ones that are required to activate the boss
        if (activatable) {
            foreach (GameObject i in weakSpots)
            {
                WeakSpot ws = i.GetComponent<WeakSpot>();
                if (!ws.canActivateBoss) {
                    ws.IsActive = false;
                }
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
            activated = true;
            hm.DamageBoss(health - healthCheck);
            health = healthCheck;   
        }

        //(Elliot) If the boss is activated, enable the health bar and set all weak spots to active
        if (activated) {
            hm.ActivateBoss(bossName);
            EnableWeakSpots();
        }

        //Debug.Log(health); //Uncomment to see how boss health changes as damage is done to the weak spots
    }

    void EnableWeakSpots()
    {
        foreach (GameObject i in weakSpots)
        {
            WeakSpot ws = i.GetComponent<WeakSpot>();
            ws.IsActive = true;
        }
    }
}
