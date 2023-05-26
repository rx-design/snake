using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource menuMusicSource;
    public AudioSource gameMusicSource;
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
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        ToggleMute(Settings.HasSound());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Settings.SoundUpdated.AddListener(ToggleMute);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Settings.SoundUpdated.RemoveListener(ToggleMute);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            menuMusicSource.Play();
            gameMusicSource.Stop();
        }
        else
        {
            menuMusicSource.Stop();
            gameMusicSource.Play();
        }
    }

    private void ToggleMute(bool hasSound)
    {
        menuMusicSource.mute = hasSound;
        gameMusicSource.mute = hasSound;
        sfxSource.mute = hasSound;
    }

    public void PlaySound(int i)
    {
        sfxSource.PlayOneShot(sounds[i]);
    }
}
