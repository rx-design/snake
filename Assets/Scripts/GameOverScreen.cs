namespace MyGameNamespace
{
    using UnityEngine;
    using TMPro;

    public class GameOverScreen : MonoBehaviour
    {
        public TMP_Text messageText;
        public TMP_Text pointsText;

        private void Start()
        {
            PointsManager.instance.UpdatePoints();
        }

        public void ShowGameOverScreen(int score)
        {
            messageText.text = "Game Over!";
            pointsText.text = "Final Points: " + score.ToString();
        }
    }
}
