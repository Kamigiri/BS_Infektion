using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;
    public int amount = 10;
    public int infected = 3;
    private float minX, maxX, minY, maxY;
    private bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = -15.08f;
        maxX = 5.77f;
        minY = -4.49f;
        maxY = 8.37f;


       
    }

    public void PauseTheGame()
    {
        bool currentGameStatus = isGameActive;

        if (currentGameStatus)
        {
            Time.timeScale = 0;
            isGameActive = false;
        } else if (!currentGameStatus)
        {
            Time.timeScale = 1;
            isGameActive = true;
        }

    }

   public void StopTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartTheGame()
    {
        isGameActive = true;
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
    }
}
