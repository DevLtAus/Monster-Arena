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
    Slider bSlider;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = playerMaxHealth;
        sceneChanger = gameObject.GetComponent<SceneChanger>();
    }

    void Awake()
    {
        GameObject bossHealth = GameObject.Find("BossHealth");
        bSlider = bossHealth.GetComponentInChildren<Slider>();
        
        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    // (Elliot) Damage the boss and initiate win condition when boss health reaches 0
    public void DamageBoss(float damage)
    {
        bHealth -= damage;
        if (bHealth <= 0) {
            bHealth = 0;
            // (Lucas) Will uncomment once win screen is implemented.
            //sceneChanger.Win();
        }
        bSlider.value = bHealth;
    }

    // (Elliot) Set boss' health and assign it to the health bar, filled
    public void SetBossHealth(float hp)
    {
        bHealth = hp;
        bSlider.maxValue = bHealth;
        bSlider.value = bHealth;
    }

    // (Lucas) Player health
    public int pHealth;
    public int playerMaxHealth;
    Slider pSlider;

    public void DamagePlayer(int damage)
    {
        pHealth -= damage;
        if (pHealth <= 0) {
            pHealth = 0;
            sceneChanger.Lose();
        }
        pSlider.value = pHealth;
    }

    public void SetPlayerHealth(int hp)
    {
        pHealth = hp;
        playerMaxHealth = hp;
        pSlider.maxValue = pHealth;
        pSlider.value = pHealth;
    }

    public void Update()
    {

    }

}
