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
        if (Save.globalSaveData.GetVolumeValues() != null)
        {
            Dictionary<string, float> volumes = Save.globalSaveData.GetVolumeValues();
            if (!volumes.TryGetValue("Master_Volume", out masterVolumeLevel))
                masterVolumeLevel = 1;
            if (!volumes.TryGetValue("SoundFX_Volume", out soundFXVolumeLevel))
                soundFXVolumeLevel = 1;
            if (!volumes.TryGetValue("Music_Volume", out musicVolumeLevel))
                musicVolumeLevel = 1;
        }
        else
        {
            masterVolumeLevel = 1;
            soundFXVolumeLevel = 1;
            musicVolumeLevel = 1;
        }

        SetAllVolumeLevels();
    }

    public void SetAllVolumeLevels()
    {
        SetMasterVolume(masterVolumeLevel);
        SetSoundFXVolume(soundFXVolumeLevel);
        SetMusicVolume(musicVolumeLevel);
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
