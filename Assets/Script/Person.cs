using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour

{
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;
    public GameObject recoveredPersonPrefab;
    public int infectedTime = 0;
    private int nextUpdate = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            UpdateEverySecond();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.name == "infectedPerson(Clone)" && this.gameObject.name == "person(Clone)")
        {
                GameObject infectedPerson = Instantiate(infectedPersonPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                Person person = infectedPerson.GetComponent<Person>();
                person.infectedTime = 1;
                Destroy(this.gameObject);  
            
        }
    }

    void UpdateEverySecond()
    {
        if (infectedTime > 0)
        {
            infectedTime++;
        }

        if (infectedTime == 15)
        {
            infectedTime = 0;
            Instantiate(recoveredPersonPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
