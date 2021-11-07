using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject creditsCanvas;

    public void CreditsScreen()
    {
        menuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void MainMenu()
    {
        menuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    public void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        creditsCanvas = GameObject.Find("CreditsCanvas");

        creditsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
