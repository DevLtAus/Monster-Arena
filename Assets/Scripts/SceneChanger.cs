using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneChanger : MonoBehaviour
{
    // (Lucas) The current scene
    private Scene curScene;

    // (Lucas) the name of the arena's scene.
    public string arenaName;
    // (Lucas) the name of the tutorial's scene.
    public string tutorialName;
    // (Elliot) the name of the menu scene.
    public string menuName;

    // (Lucas) the name of the win and lose screens' scene.
    public string winName;
    public string loseName;

    // (Lucas) Setting up the ability to reload back to a previous part of the fight.
    // (Lucas) Maybe going to store the position and health of the player each time
    // (Lucas) a monster part breaks to reload to that point.
    private HealthManager hm;
    public GameObject boss = null;
    private PhaseManager pm;

    // (Elliot) Canvases to enable/disable on save changing
    GameObject healthCanvas;
    GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        hm = gameObject.GetComponent<HealthManager>();
        try {
            pm = boss.GetComponent<PhaseManager>();
        }
        catch
        {
        }

        healthCanvas = GameObject.Find("HealthCanvas");
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        //pauseCanvas = GameObject.Find("PauseCanvas");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menuName);
    }

    public void ArenaFromBeginning()
    {
        SceneManager.LoadScene(arenaName);
        healthCanvas.SetActive(true);
        try {
            hm.Start();
            gameOverCanvas.SetActive(false);
        }
        catch
        {
        }
    }

    public void ArenaFromQuickSave()
    {
        SceneManager.LoadScene(arenaName);
        healthCanvas.SetActive(true);
        try {
            hm.Start();
            gameOverCanvas.SetActive(false);
            pm.Phase = 1;
        }
        catch
        {
        }
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene(tutorialName);
        //gameOverCanvas.SetActive(false);
    }

    public void Lose()
    {
        SceneManager.LoadScene(loseName);
    }

    // (Lucas) Go to win screen.
    public void Win()
    {
        SceneManager.LoadScene(winName);
    }

    // Update is called once per frame
    void Update()
    {
        if (hm == null) {
            hm = gameObject.GetComponent<HealthManager>();
        }
        curScene = SceneManager.GetActiveScene();

        if (curScene.name != arenaName) {
            try {
                hm.BSlider.gameObject.SetActive(false);
            }
            catch  {
                //Debug.Log("Boss health slider is already inactive");
                //Debug.Log(ex);
            }
        }
    }
}
