using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float accelerationTime = 0.5f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            timeLeft+= accelerationTime;
            Vector3 currentPosition = transform.position;

            movement = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

        }
        
    }

    void FixedUpdate()
    {
        MoveBitch(movement);
    }

    public void MoveBitch(Vector2 movementVector)
    {
        rb2d.AddForce(movementVector * maxSpeed);
    }

}
