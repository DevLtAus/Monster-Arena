using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public void SetVolume()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
