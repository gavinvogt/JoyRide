using UnityEngine;
using System.Collections;

public class CarNPC : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject movePoint;
    private bool isMoving;
    private int moveSpeed;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;
        isMoving = false;
        moveSpeed = 4;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, step);
    }

    public void Spawn()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.linearVelocity = new Vector2(0, 5f);
        StartCoroutine(ZeroVelocity());
    }

    IEnumerator ZeroVelocity()
    {
        yield return new WaitForSeconds(.5f);
        rb.linearVelocity = Vector2.zero;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Move(3f));
        
    }

    IEnumerator Move(float seconds)
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

        yield return new WaitForSeconds(seconds);
        StartCoroutine(Move(3f));
        
    }
}
