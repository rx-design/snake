using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    public int initialSize = 4;
    public float speed = 1.0f;
    public float speedMultiplier = 1.0f;

    private readonly List<Transform> _segments = new List<Transform>();
    private Vector2 _input;
    private Vector2 _direction = Vector2.right;
    private float _nextUpdate;
    private bool _gameOver;

    [SerializeField]
    private PointsManager pointsManager;

    public Canvas gameOverCanvas;

    private void Start()
    {
        ResetState();
        pointsManager.UpdateHealth();
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_gameOver)
        {
            return;
        }

        if (_direction.x != 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _input = Vector2.down;
            }
        }
        else if (_direction.y != 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _input = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _input = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_gameOver)
        {
            return;
        }

        if (_input != Vector2.zero)
        {
            _direction = _input;
        }

        for (var i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        var position = transform.position;

        transform.position = new Vector3(
            Mathf.Round(position.x) + _direction.x,
            Mathf.Round(position.y) + _direction.y,
            0.0f
        );

        _nextUpdate = Time.time + 1.0f / (speed * speedMultiplier);
    }

    public bool Occupies(Vector3 position)
    {
        return _segments
            .Any(s => s.position.Equals(position));
    }

    public void RestartGame()
    {
        ResetState();
        gameOverCanvas.gameObject.SetActive(false);
    }


    private void Grow()
    {
        var segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }


    private void ResetState()
    {
        GameManager.StartOver();
        for (var i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (var i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector3.zero;

        _gameOver = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_gameOver)
        {
            return;
        }

        if (other.CompareTag("Food"))
        {
            Food f = other.gameObject.GetComponent<Food>();
            if (f.good)
            {
                Grow();
                GameManager.AddPoints();
                pointsManager.UpdatePoints();
                return;
            }
            GameManager.RemoveHealth();
            pointsManager.UpdateHealth();
            if (GameManager.health <= 0)
            {
                _gameOver = true;
                gameOverCanvas.gameObject.SetActive(true);
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            _gameOver = true;
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
}

