using UnityEngine;

public static class GameManager
{
    private const int startingHealth = 5;
    public static int health = startingHealth;
    public static int points = 0;

    public static void RemoveHealth()
    {
        health--;
        Debug.Log(health);
    }

    public static void AddPoints()
    {
        points++;
        Debug.Log(points);
    }

    public static void StartOver()
    {
        health = startingHealth;
        points = 0;
    }
}
