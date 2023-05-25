using UnityEngine;
using TMPro;

public class HintTable : MonoBehaviour
{
    public TMP_Text hintText;
    public GameObject hintPanel;

    // Show the hint panel and set the hint text
    public void ShowHint(string hint)
    {
        hintText.text = hint;
        hintPanel.SetActive(true);
    }

    // This function can be called to hide the hint panel
    public void HideHint()
    {
        Debug.Log("Hiding hint");
        hintPanel.SetActive(false);
    }
}
