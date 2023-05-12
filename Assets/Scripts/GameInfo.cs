using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public TMP_Text lives;
    public TMP_Text score;
    public TMP_Text chars;

    private void OnEnable()
    {
        GameManager.LivesUpdated.AddListener(UpdateLives);
        GameManager.ScoreUpdated.AddListener(UpdateScore);
        GameManager.CharsUpdated.AddListener(UpdateChars);
    }

    private void UpdateLives(int value)
    {
        lives.text = $"Lives: {value}";
    }

    private void UpdateScore(int value)
    {
        score.text = $"Score: {value}";
    }

    private void UpdateChars(char[] _, char[] letters)
    {
        chars.text = $"{new string(letters)}";
    }
}