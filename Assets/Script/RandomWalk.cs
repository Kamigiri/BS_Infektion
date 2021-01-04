using UnityEngine;
using UnityEngine.UI;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float speed;

    private InputField speedInput;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speedInput = GameObject.Find("SpeedInput").GetComponent<InputField>();
        speed = System.Convert.ToInt32(speedInput.text) * 2;


        if (GameHandler.getRandomwalk())
            InvokeRepeating("ClassicMove", 0f, 1f);
        else
            InvokeRepeating("AutoMove", 0f, 1f);


    }

    public void Move(Vector2 movementVector)
    {
        rb2d.AddForce(movementVector * speed);
    }



    private void AutoMove()
    {
        Move(RandomVector());
    }

    private Vector2 RandomVector()
    {
        float radAgnle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 vecRnd = new Vector2(Mathf.Cos(radAgnle), Mathf.Sin(radAgnle));
        return vecRnd;
    }

    private void ClassicMove()
    {
        Vector3 vec = new Vector3(1f, 0);
        transform.position = transform.position + Quaternion.Euler(0, 0, Random.Range(0, 360)) * vec;
    }
}