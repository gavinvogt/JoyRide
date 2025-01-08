using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", NormalizeVolume(level));
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", NormalizeVolume(level));
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", NormalizeVolume(level));
    }

    // Normalize volume from decibels so slider level changes volume linearly
    private float NormalizeVolume(float level) => Mathf.Log10(level) * 20f;
}
