using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class CarNPC : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject movePoint;
    private int moveSpeed;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;
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
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        movePoint.transform.position = new Vector3(this.transform.position.x + Random.Range(-2.0f, 2.0f), this.transform.position.y + 4.5f, this.transform.position.z);
        StartCoroutine(FinishSpawn());
    }

    IEnumerator FinishSpawn()
    {
        yield return new WaitForSeconds(0.75f);
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
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
        movePoint.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x + xOffset, -4f, 4f), Mathf.Clamp(this.transform.position.y + yOffset, -4f, 4f), this.transform.position.z);

        yield return new WaitForSeconds(seconds);
        StartCoroutine(Move(3f));
        
    }
}
