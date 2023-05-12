using Enums;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Food : MonoBehaviour
{
  public static readonly UnityEvent<Food> IsTaken = new();
  public Letter letter;
  public TMP_Text letterDisplay;

  void Start()
  {
    letterDisplay.text = letter.ToString();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Player")) return;
    IsTaken?.Invoke(this);
  }
}