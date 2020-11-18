using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boundariesv2 : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // If you want the min max values to update if the resolution changes 
        // set them in update else set them in Start


        minX = -30f;
        maxX = 20f;
        minY = -20f;
        maxY = 10.5f;

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
        }

        if (pos.x > maxX - objectWidth)
        {
            pos.x = maxX - objectWidth;
        }

        // vertical contraint
        if (pos.y < minY + objectHeight)
        {
            pos.y = minY + objectHeight;
        }
        if (pos.y > maxY - objectHeight)
        {
            pos.y = maxY - objectHeight;
        }

        // Update position
        transform.position = pos;
    }
}
