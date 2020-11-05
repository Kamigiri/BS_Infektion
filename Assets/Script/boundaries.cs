using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boundaries : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private Rigidbody2D rb2d;
    public float push = 5f;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // If you want the min max values to update if the resolution changes 
        // set them in update else set them in Start
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;
        
}

    void Update()
    {

        // Get current position
        Vector3 pos = transform.position;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        // Horizontal contraint
        if (pos.x < minX + objectWidth)
        {
            
            pos.x = minX + objectWidth;
            GetComponent<RandomWalk>().MoveBitch(new Vector2(push, 0f));
        }

        if (pos.x > maxX - objectWidth)
        {
            pos.x = maxX - objectWidth;
            GetComponent<RandomWalk>().MoveBitch(new Vector2(push * -1f, 0f));
        }

        // vertical contraint
        if (pos.y < minY + objectHeight)
        {
            pos.y = minY + objectHeight;
            GetComponent<RandomWalk>().MoveBitch(new Vector2(0f, push));
        }
        if (pos.y > maxY - objectHeight)
        {
            pos.y = maxY - objectHeight;
            GetComponent<RandomWalk>().MoveBitch(new Vector2(0f, push * -1));
        }

            // Update position
            transform.position = pos;
    }
}
