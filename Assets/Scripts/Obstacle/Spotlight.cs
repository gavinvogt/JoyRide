using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameObject movePoint;
    private int moveSpeed;
    private Helicopter heli;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;
        moveSpeed = 4;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, step);
    }

    public void Spawn(Helicopter parentHeli)
    {
        gameObject.transform.position = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
        heli = parentHeli;
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        float xOffset = Random.Range(-2f, 2f);
        while (xOffset > -1f && xOffset < 1f)
        {
            xOffset = Random.Range(-2f, 2f);
        }

        float yOffset = Random.Range(-1.5f, 1.5f);
        while (yOffset > -.5f && yOffset < .5f)
        {
            yOffset = Random.Range(-1.5f, 1.5f);
        }
        movePoint.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x + xOffset, -4f, 4f), Mathf.Clamp(this.transform.position.y + yOffset, -4f, 4f));

        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());
    }

    public void Despawn()
    {
        Destroy(movePoint);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            StopAllCoroutines();
            heli.ShootMissile();
        }
    }
}
