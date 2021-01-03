using UnityEngine;
using UnityEngine.UI;

public class RandomWalk : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float accelerationTime = 0.06f;
    private float gameTimer;
    private float speed;
    private int seconds;

    private InputField speedInput;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speedInput = GameObject.Find("SpeedInput").GetComponent<InputField>();
        speed = System.Convert.ToInt32(speedInput.text);
    }

    private void FixedUpdate()
    {
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
}