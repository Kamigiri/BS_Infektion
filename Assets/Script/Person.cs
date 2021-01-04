using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour

{
    public GameObject personPrefab;
    public GameObject infectedPersonPrefab;
    public GameObject recoveredPersonPrefab;

    private Toggle recoveryToggle;
    private InputField recoveryDuration;

    public int infectedTime = 0;


    private void Start()
    {
        gameObject.tag = "Player";

        recoveryToggle = GameObject.Find("RecoveryToggle").GetComponent<Toggle>();
        recoveryDuration = GameObject.Find("RecoveryInput").GetComponent<InputField>();

        InvokeRepeating("UpdateEverySecond", 0f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "infectedPerson(Clone)" && this.gameObject.name == "person(Clone)")
        {
            GameObject infectedPerson = Instantiate(infectedPersonPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            Person person = infectedPerson.GetComponent<Person>();
            person.infectedTime = 1;
            Destroy(this.gameObject);
        }
    }

    private void UpdateEverySecond()
    {
        if (infectedTime > 0)
        {
            infectedTime++;
        }
        if (recoveryToggle.isOn)
            if (infectedTime - 1 >= System.Convert.ToInt32(recoveryDuration.text))
            {
                infectedTime = 0;
                Instantiate(recoveredPersonPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Destroy(gameObject);
            }
    }
}