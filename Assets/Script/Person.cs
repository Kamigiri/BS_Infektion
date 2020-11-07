using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour

{
    public int chance_of_infection;
    private SpriteRenderer spr;

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

        if (coll.gameObject.name == "person(Clone)")
        {
            int infection_chance = Random.RandomRange(0, 100);

            if (infection_chance <= chance_of_infection)
            {
                spr.color = new Color(169, 0, 0);
            }     
            
        }
    }
}
