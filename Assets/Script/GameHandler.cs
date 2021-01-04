using System;
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

    public Toggle divideToggle;
    public Toggle recoveryTogle;

    public InputField customFilePath;
    public InputField personSum;
    public InputField duration;
    public InputField speed;
    public InputField recovery;

    public Button start;

    GameObject[] inputs;

    //GameLogic
    public static bool useAlternateRndWalk = false;

    private float minX, maxX, minY, maxY, gameTimer;
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private int seconds, second;
    public static int personCounter = 0, infectedPersonCounter = 0, recoveredPersonCounter = 0;

    //Export
    private List<string> data = new List<string>();

    private void Start()
    {
        minX = -40f;
        maxX = 35f;
        minY = -24.8f;
        maxY = 9f;
        data.Add("Zyklus, Gesunde_Personen, Erkrankte_Personen, Genesene_Personen");

        SwitchRandomWalkLabel();

        inputs = GameObject.FindGameObjectsWithTag("Input");
    }

    private void Update()
    {
        if (!isGameActive)
            LimitInput();

        if (isGameActive && !isGamePaused)
        {
            gameTimer += Time.deltaTime;
            seconds = System.Convert.ToInt32(gameTimer % 60);

            

            if (seconds >= System.Convert.ToInt32(duration.text))
                EndTheGame();
        }
        Updater();
        UpdateLabels();
        CheckForRandomWalk();
    }

    private void LimitInput()
    {
        foreach(GameObject input in inputs)
            if (input.GetComponent<InputField>().text.Length > 0 && input.GetComponent<InputField>().text[0] == '-')
                input.GetComponent<InputField>().text = input.GetComponent<InputField>().text.Remove(0, 1);



        if (System.Convert.ToInt32(personSum.text) > 250)
            personSum.text = "250";

        if (System.Convert.ToInt32(recovery.text) >= System.Convert.ToInt32(duration.text))
            recovery.text = System.Convert.ToInt32(duration.text) - 1 + "";

        if (System.Convert.ToInt32(duration.text) >= 180)
            duration.text = "60";
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

    public static bool getRandomwalk()
    {
        return useAlternateRndWalk;
        
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
        ExportData.exportData(data, customFilePath.text);
    }

    public void StopTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartTheGame()
    {
        if (!isGameActive)
        {
            int infected = UnityEngine.Random.Range(1, System.Convert.ToInt32(personSum.text) /2);
            int unaffected = System.Convert.ToInt32(personSum.text) - infected;

            for (int i = 0; i < unaffected; i++)
            {
                Instantiate(personPrefab, new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0), Quaternion.identity);
            }

            for (int i = 0; i < infected; i++)
            {
                GameObject infectedPerson = Instantiate(infectedPersonPrefab, new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0), Quaternion.identity);
                Person person = infectedPerson.GetComponent<Person>();
                person.infectedTime = 1;
            }
            Time.timeScale = 1;
            isGameActive = true;
            HideUi();
            InvokeRepeating("PersonCounter", 0f, 1f);
            if (divideToggle.isOn)
                InvokeRepeating("moveBlocker", 3f, 1f);

            InvokeRepeating("Graph", 1f, 1f);

            start.interactable = false;
        }
    }

    private void Graph()
    {
        GetComponent<Graph>().buildGraph(seconds);
    }
    private void moveBlocker()
    {
        GameObject[] blocker = GameObject.FindGameObjectsWithTag("Blocker");

        foreach (GameObject block in blocker)
        {
            if (block.transform.localScale.y > 0f)
            {
                block.transform.localScale -= new Vector3(0f, 2f, 0f);

                if (block.name == "BlockerDown")
                    block.transform.position -= new Vector3(0f, 1f, 0f);

                if (block.name == "BlockerUp")
                    block.transform.position += new Vector3(0f, 1f, 0f);
            }
        }
    }

    private void Updater()
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
                    if (personCounter < System.Convert.ToInt32(personSum.text))
                        personCounter++;
                    break;

                case "infectedPerson(Clone)":
                    if (infectedPersonCounter < System.Convert.ToInt32(personSum.text))
                        infectedPersonCounter++;
                    break;

                case "recoveredPerson(Clone)":
                    if (recoveredPersonCounter < System.Convert.ToInt32(personSum.text))
                        recoveredPersonCounter++;
                    break;
            }
        }
    }

    private void PersonCounter()
    {
       

        string row = System.String.Format("{0},{1},{2},{3}", seconds + 1, personCounter, infectedPersonCounter, recoveredPersonCounter);
        data.Add(row);
    }

    public void HideUi()
    {
        GameObject[] blocker = GameObject.FindGameObjectsWithTag("Blocker");
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");

        if (!divideToggle.isOn)
            foreach (GameObject block in blocker)
                block.SetActive(false);

        if (getRandomwalk())
            foreach (GameObject edge in edges)
                edge.SetActive(false);

        personSum.DeactivateInputField();
        duration.DeactivateInputField();
        speed.DeactivateInputField();
        recovery.DeactivateInputField();

        divideToggle.interactable = false;
        recoveryTogle.interactable = false;
    }

    public void CheckForRandomWalk()
    {
        
    }
}