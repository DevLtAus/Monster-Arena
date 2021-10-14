using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // (Lucas) curScene is just the number of the scene in the build settings.
    // (Lucas) number correlations will probably change as scenes are added.
    private int curScene;

    // (Lucas) the name of the arena's scene.
    public string arenaName;
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

    public void Lose()
    {
        SceneManager.LoadScene(loseName);
    }

    // (Lucas) Go to win screen.
    public void Win()
    {
        SceneManager.LoadScene(winName);
    }
}
