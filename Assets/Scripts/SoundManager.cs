using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;

    public AudioSource sfxSource;
    public List<AudioClip> sounds;

    /*
    0 - Click
    1 - Bite
    2 - Wrong
    3 - Loss
    4 - Success
    */

    private void Awake()
    {
        musicSource.mute = Settings.HasSound();
        sfxSource.mute = Settings.HasSound();
    }

    private void OnEnable()
    {
        Settings.SoundUpdated.AddListener(OnSoundSettingUpdated);
    }

    private void OnDisable()
    {
        Settings.SoundUpdated.RemoveListener(OnSoundSettingUpdated);
    }

    private void OnSoundSettingUpdated(bool hasSound)
    {
        musicSource.mute = hasSound;
        sfxSource.mute = hasSound;
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void PlaySound(int i)
    {
        sfxSource.PlayOneShot(sounds[i]);
    }
}
