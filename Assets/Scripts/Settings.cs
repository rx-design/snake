using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static readonly UnityEvent<string> LanguageLevelUpdated = new();
    public static readonly UnityEvent<bool> SoundUpdated = new();
    public static readonly UnityEvent<int> SpeedMultiplierUpdated = new();

    public Toggle[] languageLevels;
    public Toggle[] speedMultipliers;
    public Toggle soundToggle;
    public bool isSimplified;

    private void Awake()
    {
        if (!isSimplified)
        {
            var languageLevel = GetLanguageLevel();
            foreach (var toggle in languageLevels)
            {
                if (toggle.name == languageLevel.ToString())
                {
                    toggle.isOn = true;
                }
            }
        }

        var speedMultiplier = GetSpeedMultiplier();
        speedMultipliers[speedMultiplier].isOn = true;

        soundToggle.isOn = HasSound();
    }

    private void Start()
    {
        if (!isSimplified)
        {
            foreach (var toggle in languageLevels)
            {
                toggle.onValueChanged.AddListener(ChangeLanguageLevel);
            }
        }

        foreach (var toggle in speedMultipliers)
        {
            toggle.onValueChanged.AddListener(ChangeSpeedMultiplier);
        }

        soundToggle.onValueChanged.AddListener(ToggleSound);
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

    public static int GetSpeedMultiplier()
    {
        return PlayerPrefs.GetInt("SpeedMultiplier", 0);
    }

    public void ChangeSpeedMultiplier(bool _)
    {
        for (var i = 0; i < speedMultipliers.Length; i++)
        {
            if (!speedMultipliers[i].isOn)
            {
                continue;
            }

            ChangeSpeedMultiplier(i);
        }
    }

    public static void ChangeSpeedMultiplier(int value)
    {
        PlayerPrefs.SetInt("SpeedMultiplier", value);
        PlayerPrefs.Save();

        SpeedMultiplierUpdated.Invoke(value);
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

    public void ShowSettings()
    {
        gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        gameObject.SetActive(false);
    }
}