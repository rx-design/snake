using Enums;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gamePause;
    public GameObject gameResult;
    public GameObject gameStartScreen;

    private bool _isPlaying;
    private bool _isPaused;

    private void OnEnable()
    {
        GameManager.GameStarted.AddListener(OnGameStarted);
        GameManager.GameEnded.AddListener(OnGameEnded);
    }

    private void Start()
    {
        gamePause.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
        if (!_isPlaying && Input.anyKey)
        {
            gameStartScreen.gameObject.SetActive(false);
            _isPlaying = true;
            Time.timeScale = 1.0f;

        }
    }

    private void TogglePause()
    {
        if (_isPaused)
        {
            gamePause.gameObject.SetActive(false);
            gameInfo.gameObject.SetActive(true);

            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;

            gameInfo.gameObject.SetActive(false);
            gamePause.gameObject.SetActive(true);
        }

        _isPaused = !_isPaused;
    }


    private void OnGameStarted()
    {
        gameResult.gameObject.SetActive(false);
        gameInfo.gameObject.SetActive(true);
        gameStartScreen.gameObject.SetActive(true);

        _isPlaying = false;
    }

    private void OnGameEnded(Result result, int score)
    {
        gameInfo.gameObject.SetActive(false);
        gameResult.gameObject.SetActive(true);

        _isPlaying = false;
    }
}