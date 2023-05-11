using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gamePause;
    public GameObject gameResult;
    public GameObject gameStartScreen;
    public GameObject dialogueScreen;

    private bool _isPreStart = true;
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
        if (dialogueScreen.gameObject.activeInHierarchy)
        {
            return;
        }

        if (_isPreStart && Input.anyKey)
        {
            gameStartScreen.gameObject.SetActive(false);
            _isPlaying = true;
            _isPreStart = false;
            Time.timeScale = 1.0f;

        }
        else if (_isPlaying && Input.GetKeyDown(KeyCode.Space))
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


    private void OnGameStarted()
    {
        gameResult.gameObject.SetActive(false);
        gameInfo.gameObject.SetActive(true);
        gameStartScreen.gameObject.SetActive(true);

        _isPreStart = true;
    }

    private void OnGameEnded(Result result, int score)
    {
        gameInfo.gameObject.SetActive(false);
        gameResult.gameObject.SetActive(true);

        _isPlaying = false;
    }
}