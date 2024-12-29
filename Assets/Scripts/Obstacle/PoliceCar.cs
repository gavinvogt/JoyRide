using UnityEngine;
using System.Collections;

public class PoliceCar : Obstacle
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private Transform firePoint;
    private bool isMoving;
    private GameObject movePoint;
    private int moveSpeed;
    private bool isRotating;
    private int rotationSpeed;

    private int timesMoved;

    private int health;
    
    protected override void Awake()
    {
        base.Awake();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        isMoving = false;
        isRotating = false;
        rotationSpeed = 60;

        moveSpeed = 3;
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;

        timesMoved = 0;

        health = 40;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving == true)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, step);
        }
        if(isRotating == true)
        {
            float step = rotationSpeed * (movePoint.transform.eulerAngles.z / rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, movePoint.transform.rotation, step);
        }
    }

    public override void Spawn()
    {
        rb.linearVelocity = new Vector2(0, -5f);
        StartCoroutine(ZeroVelocity());
    }

    IEnumerator ZeroVelocity()
    {
        yield return new WaitForSeconds(.5f);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(Shoot(0));
    }

    IEnumerator Shoot(int numShot)
    {
        yield return new WaitForSeconds(.5f);
        GameObject tempOb = Instantiate(bulletPrefab, firePoint);
        tempOb.transform.parent = null;
        tempOb.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * bulletSpeed);
        numShot++;
        if(numShot < 3)
        {
            StartCoroutine(Shoot(numShot));
        } else
        {
            StartCoroutine(Move(3f));
        }
    }

    IEnumerator Move(float seconds)
    {
        float xOffset = Random.Range(-2f, 2f);
        while(xOffset > -1f && xOffset < 1f)
        {
            xOffset = Random.Range(-2f, 2f);
        }
        movePoint.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x + xOffset,-4f,4f), this.transform.position.y);
        
        isMoving = true;
        timesMoved++;
        yield return new WaitForSeconds(seconds);
        isMoving = false;
        if (timesMoved < 3)
        {
            StartCoroutine(Shoot(0));
        } else
        {
            StartCoroutine(Die());
        }
    }

    public void DecreaseHealth()
    {
        if (health > 0)
        {
            health--;
        } else
        {
            gameObject.tag = "obstacle";
            StopAllCoroutines();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isRotating = true;
        movePoint.transform.eulerAngles = new Vector3(0, 0, Random.Range(60, 91));
        yield return new WaitForSeconds(1.5f);
        isRotating = false;
        Destroy(movePoint);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * os.GetSpeed());
        os.DecreasePoliceCount();
    }
}
