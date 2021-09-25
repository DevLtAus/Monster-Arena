using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject[] weakSpots;
    private GameObject gm;
    private HealthManager hm;
    private float health = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weakSpots.Length; i++)
        {
            WeakSpot ws = weakSpots[i].GetComponent<WeakSpot>();
            health += ws.health;
        }
        gm = GameObject.Find("Game Manager");
        hm = gm.GetComponent<HealthManager>();
        hm.SetBossHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
