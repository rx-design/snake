using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{

    public TMP_Text health;

    public TMP_Text points;

    public void UpdateHealth()
    {
        health.text = "Health: " + GameManager.health.ToString();
    }

    public void UpdatePoints()
    {
        points.text = "Points: " + GameManager.points.ToString();
    }

}
