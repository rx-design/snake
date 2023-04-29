using System.Linq;
using Enums;
using Objects;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static readonly UnityEvent<int> LivesUpdated = new();
    public static readonly UnityEvent<int> ScoreUpdated = new();
    public static readonly UnityEvent<char[], char[]> CharsUpdated = new();
    public static readonly UnityEvent GameStarted = new();
    public static readonly UnityEvent<Result, int> GameEnded = new();
    public int startingLives = 5;
    public Word word;

    private int _lives;
    private int _score;
    private char[] _chars;

    private void OnEnable()
    {
        Food.IsTaken.AddListener(OnFoodIsTaken);
        Snake.IsDied.AddListener(() => OnGameOver(Result.Lose));
    }

    public void Start()
    {
        _lives = startingLives;
        _score = 0;
        _chars = word.chars.ToList().Select(_ => '_').ToArray();

        LivesUpdated?.Invoke(_lives);
        ScoreUpdated?.Invoke(_score);
        CharsUpdated?.Invoke(word.chars, _chars);
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

    private void OnGameOver(Result result)
    {
        Time.timeScale = 0.0f;
        GameEnded?.Invoke(result, _score);
    }

    private bool CheckLetter(Letter letter)
    {
        for (var i = 0; i < word.chars.Length; i++)
        {
            if (word.chars[i] != (char)letter || _chars[i] != '_') continue;
            _chars[i] = (char)letter;
            CharsUpdated?.Invoke(word.chars, _chars);

            return true;
        }

        CharsUpdated?.Invoke(word.chars, _chars);

        return false;
    }

    private void OnFoodIsTaken(Food food)
    {
        if (CheckLetter(food.letter))
        {
            IncreaseScore();
            ScoreUpdated?.Invoke(_score);

            if (!_chars.Contains('_'))
            {
                OnGameOver(Result.Win);
            }

            return;
        }

        DecreaseLives();
        LivesUpdated?.Invoke(_lives);

        if (_lives <= 0)
        {
            OnGameOver(Result.Lose);
        }
    }
}