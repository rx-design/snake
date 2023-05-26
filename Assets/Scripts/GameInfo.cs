using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public TMP_Text lives;
    public TMP_Text score;
    public TMP_Text chars;
    public TMP_Text hint;

    private void OnEnable()
    {
        GameManager.LivesUpdated.AddListener(UpdateLives);
        GameManager.ScoreUpdated.AddListener(UpdateScore);
        GameManager.CharsUpdated.AddListener(UpdateChars);
        GameManager.WordHintUpdated.AddListener(UpdateHint);
    }

    private void OnDestroy()
    {
        GameManager.LivesUpdated.RemoveListener(UpdateLives);
        GameManager.ScoreUpdated.RemoveListener(UpdateScore);
        GameManager.CharsUpdated.RemoveListener(UpdateChars);
        GameManager.WordHintUpdated.RemoveListener(UpdateHint);
    }

    private void UpdateHint(string value)
    {
        hint.text = value;
    }

    private void UpdateLives(int value)
    {
        lives.text = $"Lives: {value}";
    }

    private void UpdateScore(int value)
    {
        score.text = $"Score: {value}";
    }

    private void UpdateChars(char[] _, char[] letters, bool __)
    {
        chars.text = $"{new string(letters)}";
    }
}