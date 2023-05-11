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
        title.text = result == Result.Win ? "Victory!" : "Game over!";
        score.text = $"Final Score: {value}";

        if (result == Result.Win)
        {
            nextLevelButton.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    public void TryAgain()
    {
        gameManager.ResetScore();
        gameManager.Start();
    }
}
