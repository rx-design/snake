using UnityEngine;
using TMPro;

public class GameInfo : MonoBehaviour
{
    public TMP_Text lives;
    public TMP_Text score;

    private void UpdateLivesValue(int value)
    {
        lives.text = $"Lives: {value}";
    }

    private void UpdateScoreValue(int value)
    {
        score.text = $"Score: {value}";
    }

    private void OnEnable()
    {
        GameManager.LivesUpdated += UpdateLivesValue;
        GameManager.ScoreUpdated += UpdateScoreValue;
    }

    private void OnDisable()
    {
        GameManager.LivesUpdated -= UpdateLivesValue;
        GameManager.ScoreUpdated -= UpdateScoreValue;
    }
}