using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_Text scoreText;

    public void ShowGameOverScreen(int score)
    {
        messageText.text = "Game Over!";
        scoreText.text = "Score: " + score;
    }
}
