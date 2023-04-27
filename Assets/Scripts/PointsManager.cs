// PointsManager.cs

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public TMP_Text health;
    public TMP_Text points;

    private static PointsManager _instance;
    public static PointsManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PointsManager>();
            }
            return _instance;
        }
    }

    private int finalPoints;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth()
    {
        health.text = "Health: " + GameManager.health.ToString();
    }

    public void UpdatePoints()
    {
        points.text = "Points: " + GameManager.points.ToString();
    }
}
