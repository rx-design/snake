using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Snake snake;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
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

            transform.position = position;
            break;
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        return !snake.Occupies(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RandomizePosition();
        }
    }
}