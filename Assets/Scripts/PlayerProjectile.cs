using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log(target.tag);
        if (target.CompareTag("Car"))
        {
            Debug.Log("Jump successful");
            player.UpdateCar(target.gameObject);
            Destroy(gameObject);
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
}
