using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // (Lucas) Singleton stuff
    public static HealthManager instance;

    private SceneChanger sceneChanger;

    // (Lucas) Boss health
    private int bHealth;
    public int bossMaxHealth;

    public void DamageBoss(int damage)
    {
        bHealth -= damage;
        if (bHealth <= 0) {
            bHealth = 0;
            // (Lucas) Will uncomment once win screen is implemented.
            //sceneChanger.Win();
        }
    }

    public void SetBossHealth(int hp)
    {
        bHealth = hp;
    }

    // (Lucas) Player health
    public int pHealth;
    public int playerMaxHealth;

    public void DamagePlayer(int damage)
    {
        pHealth -= damage;
        if (pHealth <= 0) {
            pHealth = 0;
            sceneChanger.Lose();
        }
    }

    public void SetPlayerHealth(int hp)
    {
        pHealth = hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        bHealth = bossMaxHealth;
        pHealth = playerMaxHealth;
        sceneChanger = gameObject.GetComponent<SceneChanger>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}
