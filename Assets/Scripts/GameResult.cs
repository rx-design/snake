using Enums;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text score;
    public Button nextLevelButton;
    public Button playAgainButton;
    public Button mainMenuButton;

    public GameManager gameManager;

    private void OnEnable()
    {
        GameManager.GameEnded.AddListener(UpdateText);
    }

    private void UpdateText(Result result, int value)
    {
        title.text = result switch
        {
            Result.Win => "Well done.",
            Result.GameWin => "Victory!",
            _ => "Game over."
        };

        score.text = result switch
        {
            Result.Win => $"Score: {value}",
            _ => $"Final Score: {value}"
        };

        switch (result)
        {
            case Result.Win:
                nextLevelButton.gameObject.SetActive(true);
                playAgainButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(false);
                break;
            case Result.GameWin:
                nextLevelButton.gameObject.SetActive(false);
                playAgainButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(true);
                break;
            case Result.Lose:
            default:
                nextLevelButton.gameObject.SetActive(false);
                playAgainButton.gameObject.SetActive(true);
                mainMenuButton.gameObject.SetActive(true);
                break;
        }
    }

    public void TryAgain()
    {
        gameManager.RestartLevel();
    }
}