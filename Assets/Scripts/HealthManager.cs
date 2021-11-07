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
    public float pHealth;
    public int playerMaxHealth;
    Slider pSlider;
    GameObject player;
    Rigidbody2D playerBody;
    SpriteRenderer[] playerRenders;
    
    // (Lucas) Player invulnerability
    public float playerIFrames;
    private float playerIFrameTimer = 0;
    public bool playerInvuln = false;

    // (Elliot) Canvases
    public GameObject healthCanvas;
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    public void Start()
    {
        pHealth = playerMaxHealth;
        sceneChanger = gameObject.GetComponent<SceneChanger>();

        try {
            player = GameObject.Find("Player");
            playerBody = player.GetComponent<Rigidbody2D>();
            playerBody.bodyType = RigidbodyType2D.Dynamic;
            playerRenders = player.GetComponentsInChildren<SpriteRenderer>();
        }
        catch
        {
        }

        // healthCanvas = GameObject.Find("HealthCanvas");
        // gameOverCanvas = GameObject.Find("GameOverCanvas");
        gameOverCanvas.SetActive(false);
        
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

    // (Elliot) Activate Boss health manager
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

    public void DamagePlayer(float damage)
    {
        GameObject player = GameObject.Find("Player");
        playerRenders = player.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in playerRenders)
        {
            sr.color = Color.red;
        }

        switch(playerInvuln) {
            case false:
                pHealth -= damage;
                if (pHealth <= 0) {
                    pHealth = 0;
                    // (Elliot) Disable player rigidbody, deactivate health canvas and activate game over canvas
                    Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
                    GameObject playerRender = player.transform.Find("PlayerBody").gameObject;

                    playerBody.bodyType = RigidbodyType2D.Static;
                    playerRender.SetActive(false);
                    
                    gameOverCanvas.SetActive(true);
                    healthCanvas.SetActive(false);
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

            GameObject player = GameObject.Find("Player");
            try {
                playerRenders = player.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in playerRenders)
                {
                    sr.color = Color.white;
                }
            }
            catch
            {
            }
        }
    }
}
