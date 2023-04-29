using Enums;
using UnityEngine;
using TMPro;

public class GameResult : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text score;

    private void OnEnable()
    {
        GameManager.GameEnded.AddListener(UpdateText);
    }

    private void UpdateText(Result result, int value)
    {
        title.text = result == Result.Win ? "Victory!" : "Game over!";
        score.text = $"Final Score: {value}";
    }
}