using UnityEngine;
using UnityEngine.UI;

public class boundaries : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    public float push;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        minX = -43.5f;
        maxX = 36f;
        minY = -25.5f;
        maxY = 10f;
        push = 2f;
    }

    private void Update()
    {
        // Get current position
        Vector3 pos = transform.position;

        // Horizontal contraint
        if (pos.x < minX + objectWidth)
        {
            pos.x = minX + objectWidth;
            if (!GameHandler.getRandomwalk())
                GetComponent<RandomWalk>().Move(new Vector2(push, 0f));

        }

        if (pos.x > maxX - objectWidth)
        {
            pos.x = maxX - objectWidth;
            if (!GameHandler.getRandomwalk())
                GetComponent<RandomWalk>().Move(new Vector2(push * -1f, 0f));
        }

        // vertical contraint
        if (pos.y < minY + objectHeight)
        {
            pos.y = minY + objectHeight;
            if (!GameHandler.getRandomwalk())
                GetComponent<RandomWalk>().Move(new Vector2(0f, push));
        }
        if (pos.y > maxY - objectHeight)
        {
            pos.y = maxY - objectHeight;
            if (!GameHandler.getRandomwalk())
                GetComponent<RandomWalk>().Move(new Vector2(0f, push * -1));
        }

        // Update position
        if (GameHandler.getRandomwalk())
            transform.position = pos;
    }
}