using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;

    void Start()
    {
        volumeSlider.value = 0.2f;
        sfxSlider.value = 0.2f;
    }

    public void SetVolume(float value)
    {
        if (MusicManager.instance.isChaseActive)
        {
            MusicManager.instance.chaseSource.volume = value;
        }
        else
        {
            MusicManager.instance.backgroundSource.volume = value;
        }
    }

    public void SetSFXVolume(float value)
    {   
        SFXManager.instance.sfxVolume = value;
    }
}