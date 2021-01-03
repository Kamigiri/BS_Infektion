using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boundaries : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    public float push = 2f;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
        minX = -44.4f;
        maxX = 36f;
        minY = -25.8f;
        maxY = 10f;

    }

    void Update()
    {

        // Get current position
        Vector3 pos = transform.position;
        

        // Horizontal contraint
        if (pos.x < minX + objectWidth)
        {
            
            pos.x = minX + objectWidth;
            GetComponent<RandomWalk>().Move(new Vector2(push, 0f));
        }

        if (pos.x > maxX - objectWidth)
        {
            pos.x = maxX - objectWidth;
            GetComponent<RandomWalk>().Move(new Vector2(push * -1f, 0f));
        }

        // vertical contraint
        if (pos.y < minY + objectHeight)
        {
            pos.y = minY + objectHeight;
            GetComponent<RandomWalk>().Move(new Vector2(0f, push));
        }
        if (pos.y > maxY - objectHeight)
        {
            pos.y = maxY - objectHeight;
            GetComponent<RandomWalk>().Move(new Vector2(0f, push * -1));
        }

            // Update position
            transform.position = pos;
    }
}
