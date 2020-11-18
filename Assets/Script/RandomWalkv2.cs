using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkv2 : MonoBehaviour
{

    private int mSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(mSpeed * Time.deltaTime, 0);
        transform.position = transform.position + Quaternion.Euler(0, 0, Random.Range(0,360)) * vec;
    }
}
