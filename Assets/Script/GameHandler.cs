﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;
    public int amount = 10;
    public int infected = 3;
    public int timer = 20;
    public Text personTxt;
    public Text infectedPersonTxt;
    public Text recoveredPersonTxT;
    public Text timerTxT;
    private float minX, maxX, minY, maxY, gameTimer;
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = -30f;
        maxX = 20f;
        minY = -20f;
        maxY = 10.5f;

    }

    void Update()
    {
        if(isGameActive && !isGamePaused)
        {
            gameTimer += Time.deltaTime;
            seconds = System.Convert.ToInt32(gameTimer % 60);
            UpdateLabels();
        }

        if (seconds >= timer)
            EndTheGame();
    }

    private void UpdateLabels()
    {
        int personCounter = 0, infectedPersonCounter = 0, recoveredPersonCounter = 0;
        GameObject[] allPersons = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject person in allPersons)
        {
            switch (person.name.ToString())
            {
                case "person(Clone)":
                    personCounter++;
                    break;
                case "infectedPerson(Clone)":
                    infectedPersonCounter++;
                    break;
                case "recoveredPerson(Clone)":
                    recoveredPersonCounter++;
                    break;
            }

        }
        personTxt.text = "Personen: " + personCounter;
        infectedPersonTxt.text = "Infenzierte Personen: " + infectedPersonCounter;
        recoveredPersonTxT.text = "Erholte Personen: " + recoveredPersonCounter;
        timerTxT.text = "Verbleibende Zeit: " + (timer - seconds);

    }

    public void PauseTheGame()
    {
        bool currentGameStatus = isGamePaused;

        if (!currentGameStatus && isGameActive)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        } else if (currentGameStatus && isGameActive)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }

    }

    public void EndTheGame()
    {
        Time.timeScale = 0;
        isGameActive = false;
    }

   public void StopTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartTheGame()
    {
        if(!isGameActive)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(personPrefab, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
            }

            for (int i = 0; i < infected; i++)
            {
                GameObject infectedPerson = Instantiate(infectedPersonPrefab, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                Person person = infectedPerson.GetComponent<Person>();
                person.infectedTime = 1;
            }
            Time.timeScale = 1;
            isGameActive = true;
        }
        
        
    }
}
