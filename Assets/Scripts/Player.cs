using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int moveSpeed;

    // current car controlled by player, may be null while jumping
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
        car.SendMessage("SetPlayer", this);
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(
                (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0), // left/right
                (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)  // up/down
            ).normalized * moveSpeed;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (car != null)
        {
            // actions that require player to have a car
            bool jumpLeft = Input.GetKeyDown(KeyCode.Q);
            bool jumpRight = Input.GetKeyDown(KeyCode.E);
            if (jumpLeft || jumpRight)
            {
                // Player jumping out of car
                car.SendMessage("ShootPlayerProjectile", jumpLeft ? "left" : "right");
                car.SendMessage("RotateCar", jumpLeft ? "left" : "right");
                car.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -5f);
                car.GetComponent<PolygonCollider2D>().enabled = false;
                car = null;
                rb = null;
            }
        }
    }

    public void UpdateCar(GameObject newCar)
    {
        // update to new car
        car = newCar;
        rb = newCar.GetComponent<Rigidbody2D>();
        car.SendMessage("SetPlayer", this);
        car.GetComponent<CarNPC>().enabled = false;
    }

    public GameObject GetCar()
    {
        return car;
    }
}
