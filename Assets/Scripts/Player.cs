using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int moveSpeed;

    // current car controlled by player
    [SerializeField] private GameObject car;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // TODO: implement
    }

    private void Awake()
    {
        rb = car.GetComponent<Rigidbody2D>();
        car.GetComponent<Car>().player = this;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0), // left/right
            (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)  // up/down
        ).normalized * moveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: implement
    }

    private void UpdateCar(GameObject newCar)
    {
        // clean previous car
        var previousCar = car.GetComponent<Car>();
        if (previousCar != null) previousCar.player = null;

        // update to new car
        car = newCar;
        rb = newCar.GetComponent<Rigidbody2D>();
    }

    public GameObject GetCar()
    {
        return car;
    }
}
