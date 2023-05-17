using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    private static readonly Dictionary<Vector2, Dictionary<Vector2, float>> Orientations = new()
    {
        {
            Vector2.zero, new Dictionary<Vector2, float>
            {
                { Vector2.zero, 0.0f },
                { Vector2.right, 0.0f },
                { Vector2.up, 90.0f },
                { Vector2.left, 180.0f },
                { Vector2.down, 270.0f }
            }
        },
        {
            Vector2.right, new Dictionary<Vector2, float>
            {
                { Vector2.left, 0.0f },
                { Vector2.right, 0.0f },
                { Vector2.zero, 0.0f },
                { Vector2.down, 0.0f },
                { Vector2.up, -90.0f }
            }
        },
        {
            Vector2.up, new Dictionary<Vector2, float>
            {
                { Vector2.zero, 90.0f },
                { Vector2.up, 90.0f },
                { Vector2.down, 90.0f },
                { Vector2.right, 90.0f },
                { Vector2.left, 0.0f }
            }
        },
        {
            Vector2.left, new Dictionary<Vector2, float>
            {
                { Vector2.zero, 180.0f },
                { Vector2.left, 180.0f },
                { Vector2.right, 180.0f },
                { Vector2.up, 180.0f },
                { Vector2.down, 90.0f }
            }
        },
        {
            Vector2.down, new Dictionary<Vector2, float>
            {
                { Vector2.zero, 270.0f },
                { Vector2.down, 270.0f },
                { Vector2.up, 270.0f },
                { Vector2.left, 270.0f },
                { Vector2.right, 180.0f }
            }
        }
    };

    public Sprite head;
    public Sprite tail;
    public Sprite body;
    public Sprite corner;
    public SpriteRenderer spriteRenderer;
    public Vector2 Direction { get; private set; }

    public void SetDirection(Vector2 direction, Vector2 previous)
    {
        var angle = Orientations[direction][previous];
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Direction = direction;
    }

    public void Follow(SnakeSegment other, int index, int length)
    {
        var isHead = index == 0;
        var isTail = index == length - 1;
        var isTurning = Direction != other.Direction;

        if (index == 0)
            spriteRenderer.sprite = head;
        else if (index == length - 1)
            spriteRenderer.sprite = tail;
        else if (isTurning)
            spriteRenderer.sprite = corner;
        else
            spriteRenderer.sprite = body;

        if (isTurning && !isHead && !isTail)
            SetDirection(other.Direction, Direction);
        else
            SetDirection(other.Direction, Vector2.zero);

        transform.position = other.transform.position;
    }
}