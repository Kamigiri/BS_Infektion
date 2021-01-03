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
        height = 8f;
        maxY = 20.72f;
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
        

        float numberInfectedPerson = height * (currentPercentagePersonInfected / 100);
        float numberInfectedPerson2 = width * (currentPercentageX / 100);
        float currentPostionX = currentSecond * numberInfectedPerson2 + minX;  


        createGameObject(infectedPixel, numberInfectedPerson, numberInfectedPerson2, currentPostionX);

    }

    public void createGameObject(GameObject theGameObject, float number, float number2, float currentPostionX)
    {
        if(number > 0)
        {
            GameObject block = Instantiate(theGameObject, new Vector3(currentPostionX, minY, 0), Quaternion.identity);
            block.transform.localScale -= new Vector3(0f, number, 0f);
            block.transform.position += new Vector3(0f, number / 2, 0f);
            block.transform.localScale -= new Vector3(number2, 0f, 0f);
            block.transform.position += new Vector3(number2 /2, 0f, 0f);
        }
        


    }

}