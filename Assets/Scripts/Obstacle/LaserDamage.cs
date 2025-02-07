using UnityEngine;
using System.Collections;

public class LaserDamage : MonoBehaviour
{
    ArrayList recentlyDamagedCars;
    Rigidbody2D rb;

    private void Awake()
    {
        recentlyDamagedCars = new ArrayList();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            StartCoroutine(AddToRecentlyDamaged(collision));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            if (!recentlyDamagedCars.Contains(collision))
            {
                StartCoroutine(AddToRecentlyDamaged(collision));
            }
        }
    }

    IEnumerator AddToRecentlyDamaged(Collider2D collision)
    {
        recentlyDamagedCars.Add(collision);
        collision.gameObject.GetComponent<Car>().TakeDamage(GetDamageLocation(collision));

        float randomWaitingRange = Random.Range(3.0f, 4.5f);
        yield return new WaitForSeconds(randomWaitingRange);

        recentlyDamagedCars.Remove(collision);
    }

    private Vector3 GetDamageLocation(Collider2D collision)
    {
        Vector3 closestCollisionPoint = collision.GetComponent<Collider2D>().ClosestPoint(transform.position);
        return new Vector3(collision.gameObject.transform.position.x, closestCollisionPoint.y);
    }
}
