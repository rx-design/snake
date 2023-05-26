using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Snake snake;
    public Food foodPrefab;

    private readonly List<Food> _food = new();

    private void OnEnable()
    {
        GameManager.CharsUpdated.AddListener(RandomizeAll);
    }

    private void OnDestroy()
    {
        GameManager.CharsUpdated.RemoveListener(RandomizeAll);
    }

    private static Letter GetRandomLetter()
    {
        var letters = Enum.GetValues(typeof(Letter));
        var randomIndex = Random.Range(0, letters.Length);

        return (Letter)letters.GetValue(randomIndex);
    }

    private void RandomizeAll(char[] initialLetters, char[] letters, bool shouldRandomize)
    {
        if (!shouldRandomize) return;

        foreach (var oldFood in _food.Where(oldFood => oldFood != null)) Destroy(oldFood.gameObject);

        _food.Clear();

        var chars = new List<char>();

        for (var i = 0; i < letters.Length; i++)
            if (letters[i] == '_')
                chars.Add(initialLetters[i]);

        foreach (Letter letter in chars)
        {
            var newFood = Instantiate(foodPrefab, GetRandomPosition(), Quaternion.identity);
            newFood.letter = letter;
            _food.Add(newFood);
        }

        var newFakeFood = Instantiate(foodPrefab, GetRandomPosition(), Quaternion.identity);
        newFakeFood.letter = GetRandomLetter();
        _food.Add(newFakeFood);
    }

    private bool IsValidPosition(Vector3 position)
    {
        return !snake.Occupies(position) && !_food
            .Any(f => f.gameObject.transform.position.Equals(position));
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

            if (!IsValidPosition(position)) continue;

            return position;
        }
    }
}