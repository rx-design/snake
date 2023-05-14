using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button skipButton; 

    private string[] _dialogueLines;
    private int _dialogueProgress;

    public void Awake()
    {
        instance = this;
    }

    public void StartDialogue(string[] lines)
    {
        if (lines.Length < 1) return;

        if (_dialogueLines != null) return;

        _dialogueLines = lines;
        AdvanceDialogue();
        ShowDialoguePanel();
    }

    public void ContinueDialogue()
    {
        if (_dialogueProgress < _dialogueLines.Length)
            AdvanceDialogue();
        else
            EndDialogue();
    }

    public void SkipDialogue()  
    {
        EndDialogue();
    }

    private void AdvanceDialogue()
    {
        dialogueText.text = _dialogueLines[_dialogueProgress];
        _dialogueProgress++;
    }

    private void EndDialogue()
    {
        HideDialoguePanel();
        ClearText();
        _dialogueProgress = 0;
    }

    private void ClearText()
    {
        dialogueText.text = "";
        _dialogueLines = null;
    }

    private void ShowDialoguePanel()
    {
        dialoguePanel.gameObject.SetActive(true);
    }

    private void HideDialoguePanel()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
}
