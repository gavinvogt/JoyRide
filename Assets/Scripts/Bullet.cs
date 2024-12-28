using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
