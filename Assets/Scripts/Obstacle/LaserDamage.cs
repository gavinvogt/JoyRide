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
            DoDamageToCar(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            if (!recentlyDamagedCars.Contains(collision))
            {
                DoDamageToCar(collision);
            }
        }
    }

    private void DoDamageToCar(Collider2D car)
    {
        StartCoroutine(AddToRecentlyDamaged(car));
        Car carScript = car.gameObject.GetComponent<Car>();
        if (carScript.GetIsShielded())
        {
            carScript.EndAbility();
        }
        else
        {
            carScript.TakeDamage(EnemyDamage.LASER_DAMAGE, GetDamageLocation(car));
        }
    }

    IEnumerator AddToRecentlyDamaged(Collider2D collision)
    {
        recentlyDamagedCars.Add(collision);

        float randomWaitingRange = Random.Range(3.0f, 4.5f);
        yield return new WaitForSeconds(randomWaitingRange);

        recentlyDamagedCars.Remove(collision);
    }

    private Vector3 GetDamageLocation(Collider2D collision)
    {
        Vector3 closestCollisionPoint = collision.ClosestPoint(transform.position);
        return new Vector3(collision.gameObject.transform.position.x, closestCollisionPoint.y);
    }
}
