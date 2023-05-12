using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SnakeSegment : MonoBehaviour
{
    private static Dictionary<Vector2, Dictionary<Vector2, float>> _orientations;

    private SpriteRenderer _spriteRenderer;

    public Sprite head;
    public Sprite tail;
    public Sprite body;
    public Sprite corner;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(Vector2 direction, Vector2 previous)
    {
        if (_orientations == null)
        {
            SetOrientations();
        }

        var angle = _orientations[direction][previous];
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Direction = direction;
    }

    public void Follow(SnakeSegment other, int index, int length)
    {
        var isHead = index == 0;
        var isTail = index == length - 1;
        var isTurning = Direction != other.Direction;

        if (index == 0)
        {
            _spriteRenderer.sprite = head;
        }
        else if (index == length - 1)
        {
            _spriteRenderer.sprite = tail;
        }
        else if (isTurning)
        {
            _spriteRenderer.sprite = corner;
        }
        else
        {
            _spriteRenderer.sprite = body;
        }

        if (isTurning && !isHead && !isTail)
        {
            SetDirection(other.Direction, Direction);
        }
        else
        {
            SetDirection(other.Direction, Vector2.zero);
        }

        transform.position = other.transform.position;
    }

    private static void SetOrientations()
    {
        _orientations = new Dictionary<Vector2, Dictionary<Vector2, float>>(5)
        {
            { Vector2.zero, new Dictionary<Vector2, float>(5) },
            { Vector2.right, new Dictionary<Vector2, float>(5) },
            { Vector2.up, new Dictionary<Vector2, float>(5) },
            { Vector2.left, new Dictionary<Vector2, float>(5) },
            { Vector2.down, new Dictionary<Vector2, float>(5) }
        };

        _orientations[Vector2.zero].Add(Vector2.zero, 0.0f);
        _orientations[Vector2.zero].Add(Vector2.right, 0.0f);
        _orientations[Vector2.zero].Add(Vector2.up, 90.0f);
        _orientations[Vector2.zero].Add(Vector2.left, 180.0f);
        _orientations[Vector2.zero].Add(Vector2.down, 270.0f);

        _orientations[Vector2.right].Add(Vector2.zero, 0.0f);
        _orientations[Vector2.right].Add(Vector2.left, 0.0f);
        _orientations[Vector2.right].Add(Vector2.right, 0.0f);
        _orientations[Vector2.right].Add(Vector2.down, 0.0f);
        _orientations[Vector2.right].Add(Vector2.up, -90.0f);

        _orientations[Vector2.up].Add(Vector2.zero, 90.0f);
        _orientations[Vector2.up].Add(Vector2.up, 90.0f);
        _orientations[Vector2.up].Add(Vector2.down, 90.0f);
        _orientations[Vector2.up].Add(Vector2.right, 90.0f);
        _orientations[Vector2.up].Add(Vector2.left, 0.0f);

        _orientations[Vector2.left].Add(Vector2.zero, 180.0f);
        _orientations[Vector2.left].Add(Vector2.left, 180.0f);
        _orientations[Vector2.left].Add(Vector2.right, 180.0f);
        _orientations[Vector2.left].Add(Vector2.up, 180.0f);
        _orientations[Vector2.left].Add(Vector2.down, 90.0f);

        _orientations[Vector2.down].Add(Vector2.zero, 270.0f);
        _orientations[Vector2.down].Add(Vector2.down, 270.0f);
        _orientations[Vector2.down].Add(Vector2.up, 270.0f);
        _orientations[Vector2.down].Add(Vector2.left, 270.0f);
        _orientations[Vector2.down].Add(Vector2.right, 180.0f);
    }
}