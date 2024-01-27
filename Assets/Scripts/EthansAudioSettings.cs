using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class EthansAudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MainMixer;
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider EffectsVolumeSlider;
    private void Start()
    {
        if(PlayerPrefs.HasKey("MVolume") || PlayerPrefs.HasKey("MuVolume") || PlayerPrefs.HasKey("EfVolume"))
        {
            LoadMasterVolume();
            LoadMusicVolume();
            LoadEFXVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetEFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        float MasterVolume = MasterVolumeSlider.value;
        MainMixer.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MVolume", MasterVolume);
    }
    public void SetMusicVolume()
    {
        float MusicVolume = MusicVolumeSlider.value;
        MainMixer.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("MuVolume", MusicVolume);
    }
    public void SetEFXVolume()
    {
        float EffectsVolume = EffectsVolumeSlider.value;
        MainMixer.SetFloat("EffectsVolume", EffectsVolume);
        PlayerPrefs.SetFloat("EfVolume", EffectsVolume);
    }

    private void LoadMasterVolume()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MVolume");
        SetMasterVolume();
    }
    private void LoadMusicVolume()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MuVolume");
        SetMusicVolume();
    }
    private void LoadEFXVolume()
    {
        EffectsVolumeSlider.value = PlayerPrefs.GetFloat("EfVolume");
        SetMusicVolume();
    }

}
