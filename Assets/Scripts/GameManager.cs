using System.Collections.Generic;
using System.Linq;
using Enums;
using Objects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static readonly UnityEvent<int> LivesUpdated = new();
    public static readonly UnityEvent<int> ScoreUpdated = new();
    public static readonly UnityEvent<char[], char[], bool> CharsUpdated = new();
    public static readonly UnityEvent GameStarted = new();
    public static readonly UnityEvent<Result, int> GameEnded = new();
    public static readonly UnityEvent<string> WordHintUpdated = new();
    public string[] dialogue;
    public int startingLives = 5;
    public List<Word> words;

    private char[] _chars;
    private int _currentLevel = 1;
    private bool _dialogueShown;
    private int _lives;
    private int _score;
    private int _scoreAtLevelStart;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        var word = GetWord();
        _lives = startingLives;
        _chars = word.chars.ToList().Select(_ => '_').ToArray();
        _scoreAtLevelStart = _score;

        LivesUpdated?.Invoke(_lives);
        ScoreUpdated?.Invoke(_score);
        CharsUpdated?.Invoke(word.chars, _chars, true);
        WordHintUpdated?.Invoke(word.hint);
        GameStarted?.Invoke();

        if (_dialogueShown) return;
        DialogueManager.instance.StartDialogue(dialogue);
        _dialogueShown = true;
    }

    private void OnEnable()
    {
        Food.IsTaken.AddListener(OnFoodIsTaken);
        Snake.IsDied.AddListener(() => OnGameOver(Result.Lose));
    }


    public void RestartLevel()
    {
        _score = _scoreAtLevelStart;
        Start();
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        if (_currentLevel > words.Count) return;
        WordHintUpdated?.Invoke(words[_currentLevel - 1].hint);
        Start();
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void DecreaseLives()
    {
        _lives--;
    }

    private void IncreaseScore()
    {
        _score++;
    }

    private Word GetWord()
    {
        return words[_currentLevel - 1];
    }

    private void OnGameOver(Result result)
    {
        GameEnded?.Invoke(result, _score);
    }

    private bool CheckLetter(Letter letter)
    {
        var word = GetWord();

        for (var i = 0; i < word.chars.Length; i++)
        {
            if (word.chars[i] != (char)letter || _chars[i] != '_') continue;
            _chars[i] = (char)letter;
            CharsUpdated?.Invoke(word.chars, _chars, false);

            return true;
        }

        CharsUpdated?.Invoke(word.chars, _chars, true);

        return false;
    }

    private void OnFoodIsTaken(Food food)
    {
        if (CheckLetter(food.letter))
        {
            Destroy(food.gameObject);
            IncreaseScore();
            ScoreUpdated?.Invoke(_score);

            if (_chars.Contains('_')) return;

            var result = _currentLevel < words.Count
                ? Result.Win
                : Result.GameWin;

            OnGameOver(result);

            return;
        }

        DecreaseLives();
        LivesUpdated?.Invoke(_lives);

        if (_lives <= 0) OnGameOver(Result.Lose);
    }
}