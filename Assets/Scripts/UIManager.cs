using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameInfo;
    public GameObject gamePause;
    public GameObject hintPanel;
    public GameObject gameResult;
    public GameObject gameControls;
    public GameObject gameDialogue;
    public Text hintPanelText;

    private bool _isControlSchemeShown;
    private bool _isHintShown;
    private bool _isPaused;
    private bool _isPlaying;
    private bool _isPreStart = true;

    private void Start()
    {
        gamePause.gameObject.SetActive(false);
        hintPanel.SetActive(false);
        hintPanelText.enabled = false;
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
        else if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TogglePause();
            else if (Input.GetKeyDown(KeyCode.H) && _isPaused) ToggleHintPanel();
        }
    }

    private void OnEnable()
    {
        GameManager.GameStarted.AddListener(OnGameStarted);
        GameManager.GameEnded.AddListener(OnGameEnded);
        GameManager.WordHintUpdated.AddListener(UpdateHintPanel);
    }

    private void ToggleHintPanel()
    {
        _isHintShown = !_isHintShown;
        hintPanel.SetActive(_isHintShown);
        hintPanelText.enabled = _isHintShown;
    }

    private void TogglePause()
    {
        if (_isPaused)
        {
            gamePause.gameObject.SetActive(false);
            gameInfo.gameObject.SetActive(true);
            _isHintShown = false;
            hintPanel.SetActive(false);
            hintPanelText.enabled = false;

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

        if (!_isControlSchemeShown)
        {
            gameControls.gameObject.SetActive(true);
            _isControlSchemeShown = true;
        }

        _isPreStart = true;
    }

    private void OnGameEnded(Result result, int score)
    {
        gameInfo.gameObject.SetActive(false);
        gameResult.gameObject.SetActive(true);

        _isPlaying = false;
    }
}