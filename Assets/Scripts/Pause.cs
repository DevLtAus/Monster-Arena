using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // (Lucas) the name of the arena's scene.
    public string arenaName;
    // (Elliot) the name of the menu scene.
    public string menuName;

    public GameObject pauseCanvas;
    private GameObject gm;
    private SceneChanger sc;
    public Button restart;
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
        gm = GameObject.Find("Game Manager");
        sc = gm.GetComponent<SceneChanger>();

        if (SceneManager.GetActiveScene().name == "Tutorial") {
            restart.onClick.AddListener(delegate {sc.TutorialLevel();});
        }
        if (SceneManager.GetActiveScene().name == "MainArena") {
            restart.onClick.AddListener(delegate {sc.ArenaFromBeginning();});
        }
        
        mainMenu.onClick.AddListener(delegate {sc.MainMenu();});
    }

    public void PauseMenu(bool active)
    {
        pauseCanvas.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
