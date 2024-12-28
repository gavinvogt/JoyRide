using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    public Player player;
    private GameObject parentCar;
    private bool hasSwitched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Car") && target.gameObject != parentCar)
        {
            if (!hasSwitched)
            {
                player.UpdateCar(target.gameObject);
                hasSwitched = true;
                Destroy(gameObject);
            }
        }
        else if (target.CompareTag("Obstacle"))
        {
            Debug.Log("Lost game");
            Destroy(gameObject);
        }
    }

    void SetPlayer(Player player)
    {
        this.player = player;
    }

    void SetParent(GameObject parentCar)
    {
        this.parentCar = parentCar;
    }
}
