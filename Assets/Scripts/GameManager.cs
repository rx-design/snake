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

    // Add this line
    public static readonly UnityEvent GamePreparing = new UnityEvent();

    public string[] dialogue;
    public int startingLives = 5;
    public List<Word> words;
    public HintTable hintTable;

    private char[] _chars;
    private int _currentLevel;
    private bool _dialogueShown;
    private int _lives;
    private int _score;
    private int _scoreAtLevelStart;

    public void Awake()
    {
        instance = this;
        _currentLevel = GetLevel();

        // If the scene is the EndingScene, disable the GameManager
        if (SceneManager.GetActiveScene().name == "EndingScene")
        {
            this.gameObject.SetActive(false);
        }
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

        // Move this line to here from HintTable.Start()
        hintTable.ShowHint(word.hint);

        // Trigger the GamePreparing event before GameStarted event
        GamePreparing?.Invoke();
        GameStarted?.Invoke();

        if (_dialogueShown) return;
        DialogueManager.instance.StartDialogue(dialogue);
        _dialogueShown = true;
    }

    private static int GetLevel()
    {
        return Settings.GetLanguageLevel() switch
        {
            LanguageLevel.A => 1,
            LanguageLevel.B => 6,
            LanguageLevel.C => 11,
            _ => 1
        };
    }

    private void OnEnable()
    {
        Food.IsTaken.AddListener(OnFoodIsTaken);
        Snake.IsDied.AddListener(() => OnGameOver(Result.Lose));
    }

    private void OnDestroy()
    {
        Food.IsTaken.RemoveListener(OnFoodIsTaken);
        Snake.IsDied.RemoveListener(() => OnGameOver(Result.Lose));
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
        WordHintUpdated?.Invoke(GetWord().hint);
        Start();
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToEndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

    private void DecreaseLives()
    {
        _lives--;
        SoundManager.instance.PlaySound(2);
    }

    private void IncreaseScore(bool inOrder)
    {
        _score += inOrder ? 2 * _lives : 1;
        SoundManager.instance.PlaySound(1);
    }

    private Word GetWord()
    {
        return words[_currentLevel - 1];
    }

    private void OnGameOver(Result result)
    {
        GameEnded?.Invoke(result, _score);
    }

    private bool IsInOrder(int index)
    {
        return !_chars.Where((t, i) => i < index && t == '_').Any();
    }

    private int CheckLetter(Letter letter)
    {
        var word = GetWord();

        for (var i = 0; i < word.chars.Length; i++)
        {
            if (word.chars[i] != (char)letter || _chars[i] != '_') continue;
            _chars[i] = (char)letter;
            CharsUpdated?.Invoke(word.chars, _chars, false);

            return i;
        }

        CharsUpdated?.Invoke(word.chars, _chars, true);

        return -1;
    }

    private void OnFoodIsTaken(Food food)
    {
        var index = CheckLetter(food.letter);

        if (index != -1)
        {
            Destroy(food.gameObject);
            IncreaseScore(IsInOrder(index));
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