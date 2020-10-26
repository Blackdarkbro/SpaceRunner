using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// New Script
public class AudioController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider = default;
    [SerializeField] private Slider soundsSlider = default;
    
    [SerializeField] private AudioSource backgroundAudioSource = default;
    [SerializeField] private AudioSource spaceshipEngineAudioSource = default;
    [SerializeField] private AudioSource soundsAudioSource = default;
    
    [SerializeField] private AudioClip crashSound = default;
    [SerializeField] private AudioClip buttonSound = default;
    
    public void PlayCrashSound()
    {
        soundsAudioSource.PlayOneShot(crashSound);
    }

    public void PlayButtonSound()
    {
        soundsAudioSource.PlayOneShot(buttonSound);
    }
    
    public void PlayEngineSound()
    {
        spaceshipEngineAudioSource.Play();
    }

    public void ChangeMusicVolumeValue()
    {
        backgroundAudioSource.volume = musicSlider.value / 10;
    }
    
    public void ChangeSoundsVolumeValue()
    {
        var value = soundsSlider.value;
        soundsAudioSource.volume = value / 10;
        spaceshipEngineAudioSource.volume = value / 10;
    }
}
