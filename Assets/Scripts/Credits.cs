using UnityEngine;

public class Credits : MonoBehaviour
{
    public void ShowCredits()
    {
        gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        gameObject.SetActive(false);
    }
}