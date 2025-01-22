using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CarNPC : MonoBehaviour
{
    private GameObject movePoint;
    private int moveSpeed;
    private bool finishedSpawning = false;
    private Vector3 spawnDestinationLocation;
    private GameObject bottomBoundary;
    private GameObject flagCollider;
    private Rigidbody2D rb;

    private void Awake()
    {
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;
        moveSpeed = 4;

        bottomBoundary = GameObject.Find("BottomBoundary");
        flagCollider = GameObject.Find("FlagColliderForNPCs");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = moveSpeed * Time.deltaTime;
        if (finishedSpawning) 
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnDestinationLocation, step);
        }
        rb.linearVelocity = Vector2.zero;
    }

    public void Spawn()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), bottomBoundary.GetComponent<BoxCollider2D>(), true);
        spawnDestinationLocation = new Vector3(this.transform.position.x + Random.Range(-2.0f, 2.0f), this.transform.position.y + 3.5f, this.transform.position.z);
        this.gameObject.GetComponent<Car>().SetImmuneToDamage(true);
    }

    private void FinishSpawn()
    {
        finishedSpawning = true;
        ActivateCar();
        StartCoroutine(Move(3f));
    }

    public void ActivateCar()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), bottomBoundary.GetComponent<BoxCollider2D>(), false);
        this.gameObject.GetComponent<Car>().SetImmuneToDamage(false);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == flagCollider && !finishedSpawning)
        {
            FinishSpawn();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Car" && finishedSpawning)
        {
            MoveAwayFromCollision(collision.transform.position);
        }
    }

    private void MoveAwayFromCollision(Vector3 collisionLocation)
    {
        float xOffset;
        float yOffset;

        if (collisionLocation.x < gameObject.transform.position.x)
        {
            xOffset = Random.Range(1.5f, 3f);
        }
        else
        {
            xOffset = Random.Range(-3f, -1.5f);
        }
        if (collisionLocation.y < gameObject.transform.position.y)
        {
            yOffset = Random.Range(1f, 2f);
        }
        else
        {
            yOffset = Random.Range(-2f, -1f);
        }

        movePoint.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x + xOffset, -4f, 4f), Mathf.Clamp(this.transform.position.y + yOffset, -4f, 4f), this.transform.position.z);
    }

    IEnumerator Move(float seconds)
    {
        float xOffset = Random.Range(-3f, 3f);
        while (xOffset > -1.5f && xOffset < 1.5f)
        {
            xOffset = Random.Range(-3f, 3f);
        }

        float yOffset = Random.Range(-2f, 2f);
        while (yOffset > -1f && yOffset < 1f)
        {
            yOffset = Random.Range(-2f, 2f);
        }
        movePoint.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x + xOffset, -4f, 4f), Mathf.Clamp(this.transform.position.y + yOffset, -4f, 4f), this.transform.position.z);

        yield return new WaitForSeconds(seconds);
        StartCoroutine(Move(3f));
        
    }
}
