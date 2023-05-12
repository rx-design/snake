using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    public static readonly UnityEvent<Food> IsTaken = new();
    public Letter letter;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private void Start()
    {
        spriteRenderer.sprite = sprites[GetLetterIndex()];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        IsTaken?.Invoke(this);
    }

    private int GetLetterIndex()
    {
        return Array.IndexOf(Enum.GetValues(typeof(Letter)), letter);
    }
}