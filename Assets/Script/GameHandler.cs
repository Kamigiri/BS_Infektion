using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;
    public int amount = 10;
    public int infected = 3;

    // Start is called before the first frame update
    void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        for (int i = 0; i < amount; i++)
        {
            Instantiate(personPrefab, new Vector3(Random.Range(bottomCorner.x, 0), Random.Range(0, topCorner.y), 0), Quaternion.identity);
        }
        
        for (int i = 0; i < infected; i++)
        {
            Instantiate(infectedPersonPrefab, new Vector3(Random.Range(bottomCorner.x, 0), Random.Range(0, topCorner.y), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
