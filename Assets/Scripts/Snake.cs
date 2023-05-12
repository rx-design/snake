using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
  public static readonly UnityEvent IsDied = new();
  public SnakeSegment segmentPrefab;
  public int initialSize = 4;
  public float speed = 20.0f;
  public float speedMultiplier = 1.0f;

  private readonly List<SnakeSegment> _segments = new();
  private SnakeSegment _head;
  private Vector2 _input;
  private float _nextUpdate;

  private void Awake()
  {
    _head = GetComponent<SnakeSegment>();

    if (_head != null) return;
    _head = gameObject.AddComponent<SnakeSegment>();
    _head.hideFlags = HideFlags.HideInInspector;
  }

  private void OnEnable()
  {
    GameManager.GameStarted.AddListener(ResetState);
  }

  private void Update()
  {
    if (_head.Direction.x != 0.0f)
    {
      if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
      {
        _input = Vector2.up;
      }
      else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
      {
        _input = Vector2.down;
      }
    }
    else if (_head.Direction.y != 0.0f)
    {
      if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
      {
        _input = Vector2.left;
      }
      else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
      {
        _input = Vector2.right;
      }
    }
  }

  private void FixedUpdate()
  {
    if (Time.time < _nextUpdate)
    {
      return;
    }

    if (_input != Vector2.zero)
    {
      _head.SetDirection(_input, Vector2.zero);
    }

    for (var i = _segments.Count - 1; i > 0; i--)
    {
      _segments[i].Follow(_segments[i - 1], i, _segments.Count);
    }

    var direction = _head.Direction;
    var position = _head.transform.position;

    _head.transform.position = new Vector3(
        Mathf.Round(position.x) + direction.x,
        Mathf.Round(position.y) + direction.y,
        0.0f
    );

    _nextUpdate = Time.time + 1.0f / (speed * speedMultiplier);
  }

  public bool Occupies(Vector3 position)
  {
    return _segments
        .Any(s => s.transform.position.Equals(position));
  }

  private void Grow()
  {
    var segment = Instantiate(segmentPrefab);
    segment.Follow(_segments.Last(), _segments.Count, _segments.Count + 1);

    _segments.Add(segment);
  }

  private void ResetState()
  {
    _input = Vector2.zero;
    _head.SetDirection(Vector2.right, Vector2.zero);
    _head.transform.position = Vector3.zero;

    for (var i = 1; i < _segments.Count; i++)
    {
      Destroy(_segments[i].gameObject);
    }

    _segments.Clear();
    _segments.Add(_head);

    for (var i = 1; i < initialSize; i++)
    {
      _segments.Add(Instantiate(segmentPrefab));
    }

    transform.position = Vector3.zero;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Food"))
    {
      Grow();
    }
    else if (other.CompareTag("Obstacle"))
    {
      IsDied?.Invoke();
    }
  }
}