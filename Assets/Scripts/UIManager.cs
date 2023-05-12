using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gamePause;
    public GameObject hintPanel;
    public GameObject gameResult;
    public GameObject gameStartScreen;
    public GameObject dialogueScreen;
    public Text hintPanelText; // Reference to the Text component in the hint panel

    private bool _isPreStart = true;
    private bool _isPlaying;
    private bool _isPaused;
    private bool _isHintShown = false;

    private void OnEnable()
    {
        GameManager.GameStarted.AddListener(OnGameStarted);
        GameManager.GameEnded.AddListener(OnGameEnded);
        GameManager.WordHintUpdated.AddListener(UpdateHintPanel);
    }

    private void Start()
    {
        gamePause.gameObject.SetActive(false);
        hintPanel.SetActive(false);
        hintPanelText.enabled = false; // Disable the hint text at the start
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
        else if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TogglePause();
            }
            else if (Input.GetKeyDown(KeyCode.H) && _isPaused)
            {
                ToggleHintPanel();
            }
        }
    }

    private void ToggleHintPanel()
    {
        _isHintShown = !_isHintShown;
        hintPanel.SetActive(_isHintShown);
        hintPanelText.enabled = _isHintShown; // Enable/Disable the hint text along with the hint panel
    }

    private void TogglePause()
    {
        if (_isPaused)
        {
            gamePause.gameObject.SetActive(false);
            gameInfo.gameObject.SetActive(true);
            _isHintShown = false;
            hintPanel.SetActive(false);
            hintPanelText.enabled = false; // Disable the hint text when unpaused

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

    private void UpdateHintPanel(string hint)
    {
        hintPanelText.text = hint;
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
