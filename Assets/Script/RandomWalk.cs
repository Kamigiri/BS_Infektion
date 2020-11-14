using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float accelerationTime = 0.06f;
    public float speed = 10f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

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

    public void Move(Vector2 movementVector)
    {
        rb2d.AddForce(movementVector * speed);
    }

    private Vector2 RandomVector()
    {
        float radAgnle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 vecRnd = new Vector2(Mathf.Cos(radAgnle), Mathf.Sin(radAgnle));
        Debug.Log("Start");
        Debug.Log(vecRnd.ToString());
        return vecRnd;
        
    }

}
