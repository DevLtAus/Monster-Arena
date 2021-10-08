using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // (Lucas) Singleton stuff
    public static HealthManager instance;

    private SceneChanger sceneChanger;

    // (Lucas) Boss health
    private float bHealth;
    public Slider bossSlider;

    public void DamageBoss(float damage)
    {
        bHealth -= damage;
        if (bHealth <= 0) {
            bHealth = 0;
            // (Lucas) Will uncomment once win screen is implemented.
            //sceneChanger.Win();
        }
    }

    public void SetBossHealth(float hp)
    {
        bHealth = hp;
        bossSlider.maxValue = bHealth;
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
