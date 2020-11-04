using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float accelerationTime = 2f;
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
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        }
        
    }

    void FixedUpdate()
    {
        rb2d.AddForce(movement * maxSpeed / Time.deltaTime);
    }
}
