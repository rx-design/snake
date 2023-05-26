using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text score;
    public Button nextLevelButton;
    public Button playAgainButton;
    public Button mainMenuButton;
    public Button ButtonEndingScene;

    private void OnEnable()
    {
        GameManager.GameEnded.AddListener(UpdateText);
    }

    private void OnDestroy()
    {
        GameManager.GameEnded.RemoveListener(UpdateText);
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
            _ => $"Final Score: {value}\nBest Score: {GetHighScore(value)}"
        };

        switch (result)
        {
            case Result.Win:
                SoundManager.instance.PlaySound(4);
                nextLevelButton.gameObject.SetActive(true);
                playAgainButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(false);
                ButtonEndingScene.gameObject.SetActive(false);
                break;
            case Result.GameWin:
                nextLevelButton.gameObject.SetActive(false);
                playAgainButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(false);
                ButtonEndingScene.gameObject.SetActive(true);
                break;
            case Result.Lose:
            default:
                SoundManager.instance.PlaySound(3);
                nextLevelButton.gameObject.SetActive(false);
                playAgainButton.gameObject.SetActive(true);
                mainMenuButton.gameObject.SetActive(true);
                ButtonEndingScene.gameObject.SetActive(false);
                break;
        }
    }

    public void TryAgain()
    {
        GameManager.instance.RestartLevel();
    }

    private static int GetHighScore(int finalScore)
    {
        var bestScore = PlayerPrefs.GetInt("Score", 0);

        if (finalScore <= bestScore) return bestScore;
        PlayerPrefs.SetInt("Score", finalScore);
        PlayerPrefs.Save();

        return finalScore;
    }
}