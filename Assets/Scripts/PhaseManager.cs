using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private GameObject gm;
    private HealthManager hm;
    private Camera cam;

    public string bossName;
    BossHealth boss;
    GameObject[] weakSpots;
    private float camBossSize;
    public float cameraSizeIncrease;

    // (Elliot) Indicator of current phase represented by integer
    private int phase = 1; // 1 by default
    public int Phase
    {
        get { return phase; }
        set { phase = value; }
    }

    // (Elliot) Deactivates all weakspots at the start except the ones that are required to activate the boss
    void Inactive() // PHASE 0
    {
        foreach (GameObject i in weakSpots)
        {
            WeakSpot ws = i.GetComponent<WeakSpot>();
            if (!ws.canActivateBoss) {
                ws.IsActive = false;
            }
        }
    }

    // (Elliot) If the boss is activated, enable the health bar and set all weak spots to active
    void Activate() // PHASE 1
    {
        hm.ActivateBoss(bossName);
        EnableWeakSpots();

        if (cam.orthographicSize < camBossSize) {
            cam.orthographicSize += 0.05f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager");
        hm = gm.GetComponent<HealthManager>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        camBossSize = cam.orthographicSize + cameraSizeIncrease;
        boss = this.GetComponent<BossHealth>();
        weakSpots = boss.weakSpots;
    }

    // Update is called once per frame
    void Update()
    {
        // (Elliot) Switch case to perform actions based on the boss' current phase
        switch(phase) {
            case 0:
                Inactive();
                break;
            case 1:
                Activate();
                break;
        }
        
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
