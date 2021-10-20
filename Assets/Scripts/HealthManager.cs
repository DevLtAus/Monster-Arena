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
    GameObject bossHealthObject;
    Text bName;
    private float bHealth;
    Slider bSlider;
    public Slider BSlider
    {
        get { return bSlider; }
        set { bSlider = value; }
    }

    // (Lucas) Player health
    public int pHealth;
    public int playerMaxHealth;
    Slider pSlider;
    
    // (Lucas) Player invulnerability
    public float playerIFrames;
    private float playerIFrameTimer = 0;
    public bool playerInvuln = false;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = playerMaxHealth;
        sceneChanger = gameObject.GetComponent<SceneChanger>();
    }

    void Awake()
    {
        bossHealthObject = GameObject.Find("BossHealth");
        bName = GameObject.Find("Name").GetComponent<Text>();
        bSlider = bossHealthObject.GetComponent<Slider>();
        pSlider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        bossHealthObject.SetActive(false);
        
        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void ActivateBoss(string name)
    {
        bName.text = name;
        bossHealthObject.SetActive(true);
    }

    // (Elliot) Damage the boss and initiate win condition when boss health reaches 0
    public void DamageBoss(float damage)
    {
        bHealth -= damage;
        if (bHealth <= 0) {
            bHealth = 0;
            // (Lucas) Go to the win screen.
            sceneChanger.Win();
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

    public void DamagePlayer(int damage)
    {
        switch(playerInvuln) {
            case false:
                pHealth -= damage;
                if (pHealth <= 0) {
                    pHealth = 0;
                    // (Lucas) Go to the game over screen.
                    sceneChanger.Lose();
                }
                pSlider.value = pHealth;
                playerIFrameTimer = playerIFrames;
                playerInvuln = true;
                break;
            case true:
                // (Lucas) Player is invulnerable.
                //Debug.Log("Player was hit while invulnerable");
                break;
        }
    }

    public void SetPlayerHealth(int hp)
    {
        pHealth = hp;
        playerMaxHealth = hp;
        pSlider.maxValue = pHealth;
        pSlider.value = pHealth;
    }

    void FixedUpdate()
    {
        if (playerIFrameTimer > 0) {
            playerIFrameTimer -= 1;
        }
        else {
            playerInvuln = false;
        }
    }
}
