using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider volSlider;

    void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("volumeThis");
    }

    public void SetVolume ()
    {
        float sliderVal = volSlider.value;
        audioMixer.SetFloat("volumeX", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("volumeThis", sliderVal);
    }

    public void ToggleFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1024, 720, false);
        }
        else
        {
            Screen.SetResolution(1024, 720, true);
        }
    }
}
