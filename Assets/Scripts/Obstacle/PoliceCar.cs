using UnityEngine;
using System.Collections;

public class PoliceCar : Obstacle
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private Transform firePoint;

    private bool isActive;
    private enum Lane
    {
        LEFT,
        MIDDLE,
        RIGHT
    }
    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
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
        isActive = true;
    }

    IEnumerator Shoot(int numShot)
    {
        GameObject tempOb = Instantiate(bulletPrefab, firePoint);
        tempOb.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * bulletSpeed);
        yield return new WaitForSeconds(.5f);
        numShot++;
        if(numShot < 3)
        {
            StartCoroutine(Shoot(numShot));
        }

    }
}
