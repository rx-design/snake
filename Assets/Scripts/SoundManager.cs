using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;
    public List<AudioSource> sounds;
    public int index = -1;

    private void Awake()
    {
        // musicSource.mute = !Settings.HasSound();
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
        // musicSource.mute = !hasSound;
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
        sounds[i].Play();
    }
}
