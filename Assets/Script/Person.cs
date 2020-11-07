using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour

{
    public int chance_of_infection = 20;
    private SpriteRenderer spr;
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.name == "infectedPerson(Clone)" && this.gameObject.name == "person(Clone)")
        {
            int infection_chance = Random.RandomRange(0, 100);

            if (infection_chance <= chance_of_infection)
            {
                Instantiate(infectedPersonPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }     
            
        }
    }
}
