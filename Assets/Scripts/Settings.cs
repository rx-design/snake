using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static readonly UnityEvent<string> LanguageLevelUpdated = new();
    public static readonly UnityEvent<bool> SoundUpdated = new();
    public static readonly UnityEvent<bool> SpeedUpdated = new();

    public Toggle[] languageLevels;
    public Toggle soundToggle;
    public Toggle speedToggle;

    private void Awake()
    {
        var languageLevel = GetLanguageLevel();
        foreach (var toggle in languageLevels)
        {
            if (toggle.name == languageLevel.ToString())
            {
                toggle.isOn = true;
            }
        }
        soundToggle.isOn = HasSound();
        speedToggle.isOn = IsHighSpeed();
    }

    private void Start()
    {
        foreach (var toggle in languageLevels)
        {
            toggle.onValueChanged.AddListener(ChangeLanguageLevel);
        }
        soundToggle.onValueChanged.AddListener(ToggleSound);
        speedToggle.onValueChanged.AddListener(ToggleSpeed);
    }

    public static LanguageLevel GetLanguageLevel()
    {
        var value = PlayerPrefs.GetString("LanguageLevel", LanguageLevel.A.ToString());

        return Enum.Parse<LanguageLevel>(value);
    }

    public void ChangeLanguageLevel(bool _)
    {
        foreach (var toggle in languageLevels)
        {
            if (!toggle.isOn)
            {
                continue;
            }

            ChangeLanguageLevel(toggle.name);
        }
    }

    public static void ChangeLanguageLevel(string value)
    {
        PlayerPrefs.SetString("LanguageLevel", value);
        PlayerPrefs.Save();

        LanguageLevelUpdated.Invoke(value);
    }

    public static bool HasSound()
    {
        return PlayerPrefs.GetInt("Sound", 0) == 1;;
    }

    public static void ToggleSound(bool _)
    {
        var hasSound = HasSound();
        var value = hasSound ? 0 : 1;

        PlayerPrefs.SetInt("Sound", value);
        PlayerPrefs.Save();

        SoundUpdated.Invoke(!hasSound);
    }

    public static bool IsHighSpeed()
    {
        return PlayerPrefs.GetInt("HighSpeed", 0) == 1;
    }

    public static void ToggleSpeed(bool _)
    {
        var isHighSpeed = IsHighSpeed();
        var value = isHighSpeed ? 0 : 1;

        PlayerPrefs.SetInt("HighSpeed", value);
        PlayerPrefs.Save();

        SpeedUpdated.Invoke(!isHighSpeed);
    }

    public void ShowSettings()
    {
        gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        gameObject.SetActive(false);
    }
}