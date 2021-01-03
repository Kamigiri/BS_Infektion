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

    public Toggle customExport;
    public Toggle divideToggle;
    public Toggle recoveryTogle;

    public InputField customFilePath;
    public InputField personSum;
    public InputField duration;
    public InputField speed;

    //GameLogic
    public bool useAlternateRndWalk = false;

    private float minX, maxX, minY, maxY, gameTimer;
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private int seconds, second;
    public static int personCounter = 0, infectedPersonCounter = 0, recoveredPersonCounter = 0;

    //Export
    private List<string> data = new List<string>();

    private void Start()
    {
        minX = -43.77f;
        maxX = 36.44f;
        minY = -25.69f;
        maxY = 10.11f;
        data.Add("Zyklus, Gesunde_Personen, Erkrankte_Personen, Genesene_Personen");

        SwitchRandomWalkLabel();
    }

    private void Update()
    {
        if (isGameActive && !isGamePaused)
        {
            gameTimer += Time.deltaTime;
            seconds = System.Convert.ToInt32(gameTimer % 60);

            //GetComponent<Graph>().buildGraph(seconds);

            if (seconds >= System.Convert.ToInt32(duration.text))
                EndTheGame();
        }

        UpdateLabels();
        CheckForRandomWalk();
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
        if (!isGameActive && !isGamePaused)
        {
            if (useAlternateRndWalk)
                useAlternateRndWalk = false;
            else
                useAlternateRndWalk = true;

            SwitchRandomWalkLabel();
        }
           
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
    }

    public void exportToCsv()
    {
        if (customExport.isOn)
            ExportData.exportData(data, customFilePath.text);
        else
            ExportData.exportData(data, @"Excel\report.csv");
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
            InvokeRepeating("PersonCounter", 0f, 1f);
            if(divideToggle.isOn)
                InvokeRepeating("moveBlocker", 2f, 1f);
        }
    }

    private void moveBlocker()
    {
        GameObject[] blocker = GameObject.FindGameObjectsWithTag("Blocker");

        foreach (GameObject block in blocker)
        {
            if(block.transform.localScale.y > 0f)
            {
                block.transform.localScale -= new Vector3(0f, 2f, 0f);

                if (block.name == "BlockerDown")
                    block.transform.position -= new Vector3(0f, 1f, 0f);

                if (block.name == "BlockerUp")
                    block.transform.position += new Vector3(0f, 1f, 0f);
            }
           

        }

        
    }

    private void PersonCounter()
    {
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

        string row = System.String.Format("{0},{1},{2},{3}", seconds + 1, personCounter, infectedPersonCounter, recoveredPersonCounter);
        data.Add(row);
    }

    public void HideUi()
    {
        GameObject[] blocker = GameObject.FindGameObjectsWithTag("Blocker");

        if (!divideToggle.isOn)
            foreach (GameObject block in blocker)
                block.SetActive(false);
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