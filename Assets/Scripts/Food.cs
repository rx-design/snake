using Enums;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    public static readonly UnityEvent<Food> IsTaken = new();
    public Letter letter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        IsTaken?.Invoke(this);
    }
}