using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeakSpot : MonoBehaviour
{
    //(Elliot) health is public variable with value of 1 by default
    public int health = 1;
    Canvas wCanvas;
    Slider wSlider;

    // Start is called before the first frame update
    void Start()
    {
        wCanvas = GetComponentInChildren<Canvas>();
        wCanvas.enabled = false;

        wSlider = GetComponentInChildren<Slider>();
        wSlider.maxValue = health;
        wSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //(Elliot) Damage the weak spot and disable it when health reaches 0
    public void Damage(int damage)
    {
        wCanvas.enabled = true;
        
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }
        wSlider.value = health;
    }
}
