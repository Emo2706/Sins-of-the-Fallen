using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerManager : MonoBehaviour
{
    [SerializeField] Slider _masterSlider, _musicSlider, _sfxSlider;
    
    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.mixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.instance.mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
