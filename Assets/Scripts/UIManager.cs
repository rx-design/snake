using Enums;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gamePause;
    public GameObject gameResult;
    public GameObject gameControls;
    public GameObject gameDialogue;

    private bool _isControlSchemeShown;
    private bool _isPaused;
    private bool _isPlaying;
    private bool _isPreStart = true;

    private void Start()
    {
        gamePause.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameDialogue.gameObject.activeInHierarchy) return;

        if (_isPreStart && Input.anyKey)
        {
            gameControls.gameObject.SetActive(false);
            _isPlaying = true;
            _isPreStart = false;
            Time.timeScale = 1.0f;
        }
        else if (_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    private void OnEnable()
    {
        GameManager.GameStarted.AddListener(OnGameStarted);
        GameManager.GameEnded.AddListener(OnGameEnded);
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
        Time.timeScale = 0.0f;
        gameResult.gameObject.SetActive(false);
        gameInfo.gameObject.SetActive(true);

        if (!_isControlSchemeShown)
        {
            gameControls.gameObject.SetActive(true);
            _isControlSchemeShown = true;
        }

        _isPreStart = true;
    }

    private void OnGameEnded(Result result, int score)
    {
        Time.timeScale = 0.0f;
        _isPlaying = false;
        gameInfo.gameObject.SetActive(false);
        gameResult.gameObject.SetActive(true);
    }
}