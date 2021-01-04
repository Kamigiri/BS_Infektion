using System;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    private float minX, minY, height, width, maxY;

    public GameObject pixel;
    public GameObject infectedPixel;
    public GameObject recoveredPixel;

    private InputField personSum, duration;

    private int maxPerson, maxDuration;

    private void Start()
    {
        minX = -23.35f;
        minY = 12.25f;
        maxY = 20.25f;
        height = 8f;
        width = 40f;

        gameObject.tag = "Player";

        personSum = GameObject.Find("PersonInput").GetComponent<InputField>();
        duration = GameObject.Find("DurationInput").GetComponent<InputField>();




    }

    private void Update()
    {
        maxPerson = System.Convert.ToInt32(personSum.text);
        maxDuration = System.Convert.ToInt32(duration.text);
    }

    public void buildGraph(int currentSecond)
    {
        float currentPercentageX = 100f / maxDuration;
        float currentPercentagePerson = 100f / maxPerson * GameHandler.personCounter;
        float currentPercentagePersonInfected = 100f / maxPerson * GameHandler.infectedPersonCounter;
        float currentPercentagePersonRecovered = 100f / maxPerson * GameHandler.recoveredPersonCounter;





        //float currentPostionX = width * (currentPercentageX / 100) + minX;
        

        float numberInfectedPerson = height * (currentPercentagePersonInfected / 100f);
        float numberInfectedPersonX = width * (currentPercentageX / 100f);
        float numberInfectedPersonInverse = height - numberInfectedPerson;
        float currentPostionX = currentSecond * numberInfectedPersonX + minX;  


        createGameObject(infectedPixel, numberInfectedPerson, numberInfectedPersonX, numberInfectedPersonInverse, currentPostionX);

    }

    public void createGameObject(GameObject theGameObject, float number, float numberx, float numberInverse, float currentPostionX)
    {
        number = number >= 8f ? 8.5f : number;
        if (number > 0.4f)
        {
            GameObject block = Instantiate(theGameObject, new Vector3(currentPostionX, minY, 0), Quaternion.identity);
            

            block.transform.localScale -= new Vector3(0f, number, 0f);
            block.transform.position += new Vector3(0f, number / 2, 0f);
            block.transform.localScale -= new Vector3(numberx, 0f, 0f);
            block.transform.position += new Vector3(numberx / 2, 0f, 0f);
            
        }
        numberInverse = numberInverse >= 7.5f ? 7.5f : numberInverse;
        if (numberInverse > 0f)
        {
            
            GameObject blockInverse = Instantiate(theGameObject, new Vector3(currentPostionX, maxY, 0), Quaternion.identity);
            blockInverse.GetComponent<SpriteRenderer>().color = Color.white;
            blockInverse.transform.localScale += new Vector3(0f, numberInverse, 0f);
            blockInverse.transform.position -= new Vector3(0f, numberInverse / 2, 0f);
            blockInverse.transform.localScale -= new Vector3(numberx, 0f, 0f);
            blockInverse.transform.position += new Vector3(numberx / 2, 0f, 0f);
        }
       


    }

}