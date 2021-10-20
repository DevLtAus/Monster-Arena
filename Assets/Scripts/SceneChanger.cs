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

    // (Lucas) the name of the win and lose screens' scene.
    public string winName;
    public string loseName;

    // (Lucas) Setting up the ability to reload back to a previous part of the fight.
    // (Lucas) Maybe going to store the position and health of the player each time
    // (Lucas) a monster part breaks to reload to that point.
    private HealthManager hm;


    // Start is called before the first frame update
    void Start()
    {
        hm = gameObject.GetComponent<HealthManager>();
    }

    public void ArenaFromBeginning()
    {
        SceneManager.LoadScene(arenaName);
    }

    public void ArenaFromQuickSave()
    {
        // (Lucas) Will complete once weak spot health is implemented.
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene(tutorialName);
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

        // if (curScene.name == arenaName) {
        //     hm.BSlider.gameObject.SetActive(true);
        // }
        // else {
        //     try {
        //         hm.BSlider.gameObject.SetActive(false);
        //     }
        //     catch (NullReferenceException ex) {
        //         //Debug.Log("Boss health slider is already inactive");
        //     }
        // }
    }
}
