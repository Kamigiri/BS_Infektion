using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    //Objects
    public GameObject personPrefab;

    public GameObject infectedPersonPrefab;
    public GameObject recoverdPersonPrefab;

    //UI
    public Text personTxt;
    public Text infectedPersonTxt;
    public Text recoveredPersonTxT;
    public Text timerTxT;
    public Text rndWalkTxT;
    public InputField personSum;
    public InputField duration;
    public InputField speed;
    
   

    //GameLogic
    public bool useAlternateRndWalk = false;

    private float minX, maxX, minY, maxY, gameTimer;
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private int seconds;
    private int personCounter = 0, infectedPersonCounter = 0, recoveredPersonCounter = 0;

    //Export
    private List<Dictionary<string, int>> exportList = new List<Dictionary<string, int>>();

    private void Start()
    {
        minX = -43.77f;
        maxX = 36.44f;
        minY = -25.69f;
        maxY = 10.11f;

        SwitchRandomWalkLabel();
    }

    private void Update()
    {
        if (isGameActive && !isGamePaused)
        {
            gameTimer += Time.deltaTime;
            seconds = System.Convert.ToInt32(gameTimer % 60);
            //TODO nur jede Sekunde nicht jeden Frame
            PersonCounter();

            if (seconds >= System.Convert.ToInt32(duration.text))
                EndTheGame();
        }

        UpdateLabels();
        CheckForRandomWalk();
    }

    private void PersonCounter()
    {
        Dictionary<string, int> currentData =
           new Dictionary<string, int>
           {
            { "Zyklus", seconds }
           };

        GameObject[] allPersons = GameObject.FindGameObjectsWithTag("Player");
        personCounter = 0;
        infectedPersonCounter = 0;
        recoveredPersonCounter = 0;

        foreach (GameObject person in allPersons)
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
        currentData.Add("Personen", personCounter);
        currentData.Add("Infected", infectedPersonCounter);
        this.exportList.Add(currentData);
    }

    private void UpdateLabels()
    {
        SwitchRandomWalkLabel();

        personTxt.text = "Personen: " + personCounter;
        infectedPersonTxt.text = "Infizierte Personen: " + infectedPersonCounter;
        recoveredPersonTxT.text = "Erholte Personen: " + recoveredPersonCounter;
        timerTxT.text = "Verbleibende Zeit: " + (System.Convert.ToInt32(duration.text) - seconds);
    }

    public void SwitchRandomWalk()
    {
        if (useAlternateRndWalk)
            useAlternateRndWalk = false;
        else
            useAlternateRndWalk = true;

        SwitchRandomWalkLabel();
    }

    public void SwitchRandomWalkLabel()
    {
        if (useAlternateRndWalk)
            rndWalkTxT.text = "Klassisch";
        else
            rndWalkTxT.text = "Smooth";
    }

    public void PauseTheGame()
    {
        bool currentGameStatus = isGamePaused;

        if (!currentGameStatus && isGameActive)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
        else if (currentGameStatus && isGameActive)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }
    }

    public void EndTheGame()
    {
        Time.timeScale = 0;
        isGameActive = false;
        //export to Excel
        foreach (Dictionary<string, int> item in exportList)
            Debug.Log(item);
    }

    public void StopTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartTheGame()
    {
        if (!isGameActive)
        {
            int infected = Random.Range(1, System.Convert.ToInt32(personSum.text) - 1);
            int unaffected = System.Convert.ToInt32(personSum.text) - infected;

            for (int i = 0; i < unaffected; i++)
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
            HideUi();
        }
    }

    public void HideUi()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("invisibleWhilePlaying");

        foreach (GameObject item in objects)
        {
            item.SetActive(false);
        }
    }

    public void CheckForRandomWalk()
    {
        if (useAlternateRndWalk)
        {
            personPrefab.GetComponent<RandomWalk>().enabled = false;
            infectedPersonPrefab.GetComponent<RandomWalk>().enabled = false;
            recoverdPersonPrefab.GetComponent<RandomWalk>().enabled = false;

            personPrefab.GetComponent<boundaries>().enabled = false;
            infectedPersonPrefab.GetComponent<boundaries>().enabled = false;
            recoverdPersonPrefab.GetComponent<boundaries>().enabled = false;

            personPrefab.GetComponent<RandomWalkv2>().enabled = true;
            infectedPersonPrefab.GetComponent<RandomWalkv2>().enabled = true;
            recoverdPersonPrefab.GetComponent<RandomWalkv2>().enabled = true;

            personPrefab.GetComponent<boundariesv2>().enabled = true;
            infectedPersonPrefab.GetComponent<boundariesv2>().enabled = true;
            recoverdPersonPrefab.GetComponent<boundariesv2>().enabled = true;
        }
        else
        {
            personPrefab.GetComponent<RandomWalk>().enabled = true;
            infectedPersonPrefab.GetComponent<RandomWalk>().enabled = true;
            recoverdPersonPrefab.GetComponent<RandomWalk>().enabled = true;

            personPrefab.GetComponent<boundaries>().enabled = true;
            infectedPersonPrefab.GetComponent<boundaries>().enabled = true;
            recoverdPersonPrefab.GetComponent<boundaries>().enabled = true;

            personPrefab.GetComponent<RandomWalkv2>().enabled = false;
            infectedPersonPrefab.GetComponent<RandomWalkv2>().enabled = false;
            recoverdPersonPrefab.GetComponent<RandomWalkv2>().enabled = false;

            personPrefab.GetComponent<boundariesv2>().enabled = false;
            infectedPersonPrefab.GetComponent<boundariesv2>().enabled = false;
            recoverdPersonPrefab.GetComponent<boundariesv2>().enabled = false;
        }
    }
}