using UnityEngine;
using System.Collections;
using UnityEditor;

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
            StartCoroutine(addToRecentlyDamaged(collision));
            collision.gameObject.GetComponent<Car>().TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            if (!recentlyDamagedCars.Contains(collision))
            {
                StartCoroutine(addToRecentlyDamaged(collision));
                collision.gameObject.GetComponent<Car>().TakeDamage();
            }
        }
    }

    IEnumerator addToRecentlyDamaged(Collider2D collision)
    {
        recentlyDamagedCars.Add(collision);
        float randomWaitingRange = Random.Range(3.0f, 4.5f);
        yield return new WaitForSeconds(5f);
        recentlyDamagedCars.Remove(collision);
    }
}
