using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    static float masterVolumeLevel;
    static float soundFXVolumeLevel;
    static float musicVolumeLevel;

    public void SoundStartUp()
    {
        Dictionary<string, float> volumes = Save.globalSaveData.GetVolumeValues();
        SetMasterVolume(volumes.GetValueOrDefault("Master_Volume"));
        SetSoundFXVolume(volumes.GetValueOrDefault("SoundFX_Volume"));
        SetMusicVolume(volumes.GetValueOrDefault("Music_Volume"));
    }

    public void SetMasterVolume(float level)
    {
        masterVolumeLevel = level;
        audioMixer.SetFloat("masterVolume", NormalizeVolume(masterVolumeLevel));
    }

    public void SetSoundFXVolume(float level)
    {
        soundFXVolumeLevel = level;
        audioMixer.SetFloat("soundFXVolume", NormalizeVolume(soundFXVolumeLevel));
    }

    public void SetMusicVolume(float level)
    {
        musicVolumeLevel = level;
        audioMixer.SetFloat("musicVolume", NormalizeVolume(musicVolumeLevel));
    }

    public static Dictionary<string, float> GetSoundVolumeInSaveFormat()
    {
        Dictionary<string, float> volumes = new() {
            { "Master_Volume" , masterVolumeLevel },
            { "SoundFX_Volume" , soundFXVolumeLevel },
            { "Music_Volume" , musicVolumeLevel },
        };
        return volumes;
    }

    public static float GetMasterVolumeLevel()
    {
        return masterVolumeLevel;
    }
    public static float GetSoundFXVolumeLevel()
    {
        return soundFXVolumeLevel;
    }
    public static float GetMusicVolumeLevel()
    {
        return musicVolumeLevel;
    }

    // Normalize volume from decibels so slider level changes volume linearly
    public static float NormalizeVolume(float level) => Mathf.Log10(level) * 20f;
}
