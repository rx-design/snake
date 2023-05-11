using System.Collections.Generic;
using Enums;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Snake snake;
    public Food[] foods;

    private void OnEnable()
    {
        GameManager.CharsUpdated.AddListener(RandomizeAll);
    }

    private void RandomizeAll(char[] initialLetters, char[] letters)
    {
        var chars = new List<char>();

        for (var i = 0; i < letters.Length; i++)
        {
            if (letters[i] == '_')
            {
                chars.Add(initialLetters[i]);
            }
        }

        if (chars.Count == 0)
        {
            return;
        }

        foods[0].transform.position = GetRandomPosition();
        foods[0].GetComponent<SpriteRenderer>().color = Color.green;
        foods[0].letter = (Letter)chars[Random.Range(0, chars.Count - 1)];

        foods[1].transform.position = GetRandomPosition();
        foods[1].GetComponent<SpriteRenderer>().color = Color.red;
        foods[1].letter = (Letter)Random.Range(0, 25);
    }

    private bool IsValidPosition(Vector3 position)
    {
        return !snake.Occupies(position);
    }

    private Vector3 GetRandomPosition()
    {
        var bounds = gridArea.bounds;

        while (true)
        {
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var y = Random.Range(bounds.min.y, bounds.max.y);

            var position = new Vector3(
                Mathf.Round(x),
                Mathf.Round(y),
                0.0f
            );

            if (!IsValidPosition(position))
            {
                continue;
            }

            return position;
        }
    }
}