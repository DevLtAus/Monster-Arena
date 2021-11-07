using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    private AudioSource heart;

    // Start is called before the first frame update
    void Start()
    {
        heart = GetComponent<AudioSource>();
    }

    public void Beat()
    {
        heart.Play();
    }
}
