using enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void LivesUpdatedDelegate(int lives);
    public delegate void ScoreUpdatedDelegate(int score);
    public delegate void GameIsEndedDelegate(int score);
    public delegate void GameStartedDelegate();

    public static event LivesUpdatedDelegate LivesUpdated;
    public static event ScoreUpdatedDelegate ScoreUpdated;
    public static event GameIsEndedDelegate GameIsEnded;
    public static event GameStartedDelegate GameStarted;

    public int startingLives = 5;

    private int _lives;
    private int _score;

    public void Start()
    {
        _lives = startingLives;
        _score = 0;

        LivesUpdated?.Invoke(_lives);
        ScoreUpdated?.Invoke(_score);
        GameStarted?.Invoke();

        Time.timeScale = 1.0f;
    }

    private void DecreaseLives()
    {
        _lives--;
    }

    private void IncreaseScore()
    {
        _score++;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0.0f;
        GameIsEnded?.Invoke(_score);
    }

    private bool IsGood(Letter letter)
    {
        return letter == Letter.A;
    }

    private void OnFoodIsTaken(Food food)
    {
        if (IsGood(food.letter))
        {
            IncreaseScore();
            ScoreUpdated?.Invoke(_score);
            return;
        }

        DecreaseLives();
        LivesUpdated?.Invoke(_lives);

        if (_lives <= 0)
        {
            OnGameOver();
        }
    }

    private void OnEnable()
    {
        Food.IsTaken += OnFoodIsTaken;
        Snake.IsDied += OnGameOver;
    }

    private void OnDisable()
    {
        Food.IsTaken -= OnFoodIsTaken;
        Snake.IsDied -= OnGameOver;
    }
}