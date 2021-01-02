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
        // If you want the min max values to update if the resolution changes 
        // set them in update else set them in Start


        minX = -43.77f;
        maxX = 36.44f;
        minY = -25.69f;
        maxY = 10.11f;

    }

    void FixedUpdate()
    {

        // Get current position
        Vector3 pos = transform.position;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

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
