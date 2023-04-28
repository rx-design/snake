using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gameOver;
    public GameObject gamePause;

    private bool _isPaused;
    private bool _isPlaying;

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

    private void OnGameIsEnded(int score)
    {
        gameInfo.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);

        _isPlaying = false;
    }

    private void OnGameStarted()
    {
        gameOver.gameObject.SetActive(false);
        gameInfo.gameObject.SetActive(true);

        _isPlaying = true;
    }

    private void OnEnable()
    {
        GameManager.GameIsEnded += OnGameIsEnded;
        GameManager.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        GameManager.GameIsEnded -= OnGameIsEnded;
        GameManager.GameStarted -= OnGameStarted;
    }
}