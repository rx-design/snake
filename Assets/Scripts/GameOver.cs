using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text score;

    private void Awake()
    {
        GameManager.GameIsEnded += UpdateText;
    }

    private void UpdateText(int scoreValue)
    {
        title.text = "Game Over!";
        score.text = $"Final Score: {scoreValue}";
    }

    private void OnDestroy()
    {
        GameManager.GameIsEnded -= UpdateText;
    }
}