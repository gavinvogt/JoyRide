using UnityEngine;
using System.Collections;

public class PoliceCar : Obstacle
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private Transform firePoint;

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
        StartCoroutine(Shoot(0));
    }

    IEnumerator Shoot(int numShot)
    {
        yield return new WaitForSeconds(.5f);
        GameObject tempOb = Instantiate(bulletPrefab, firePoint);
        tempOb.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * bulletSpeed);
        numShot++;
        if(numShot < 3)
        {
            StartCoroutine(Shoot(numShot));
        } else
        {
            StartCoroutine(Cooldown(3f));
        }
    }

    IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(Shoot(0));
    }
}
