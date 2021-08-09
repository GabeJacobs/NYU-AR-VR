using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MusicToggle : MonoBehaviour
{
    public AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    
    public void PlayAudio()
    {
        audio.Play();
    }
    public void PauseAudio()
    {
        audio.Pause();
    }

    public void setVolume(float volume)
    {
        audio.volume = volume;
    }
}
