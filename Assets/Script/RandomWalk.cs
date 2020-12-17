using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float accelerationTime = 0.06f;
    private float timeLeft;
    private float speed;

    private Slider speedSlider;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();

    }

 

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft += accelerationTime;
            Move(RandomVector());

        }
        
    }

     void Update()
    {
        speed = speedSlider.value;
    }

    public void Move(Vector2 movementVector)
    {
        rb2d.AddForce(movementVector * speed);
    }

    private Vector2 RandomVector()
    {
        float radAgnle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 vecRnd = new Vector2(Mathf.Cos(radAgnle), Mathf.Sin(radAgnle));
        return vecRnd;
        
    }

}
