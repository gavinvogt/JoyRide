using UnityEngine;
using System.Collections;
using System;

public class Helicopter : MonoBehaviour, BaseEnemy
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float crashAngle;
    [SerializeField] private float crashSpeed;
    [SerializeField] private float hoverRangeY;

    // track how the helicopter is moving
    private bool isHovering = false;
    [SerializeField] private float hoverPeriod;
    private Vector3 initialPosition;
    private bool isRotating = false;

    // track helicopter health
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject damageInidcator;

    // track the spawner
    private HelicopterSpawner spawner;
    private string helicopterSide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.SetMaxHealth(health);
        StartHovering();
    }

    private void Update()
    {
        if (isHovering)
        {
            transform.position = new Vector3(
                transform.position.x,
                initialPosition.y + Mathf.Sin(2 * (float)Math.PI * Time.time / hoverPeriod) * hoverRangeY / 2
            );
        }
        if (isRotating)
        {
            float step = crashAngle * Time.deltaTime; // will complete rotation over 1 second
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + step
            );
        }
    }

    private void StartHovering()
    {
        isHovering = true;
        initialPosition = new Vector3(transform.position.x, transform.position.y);
    }

    public void DecreaseHealth(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
            StartCoroutine(DamageMarker());
        }
        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Crash());
        }
    }

    public void SetSpawner(HelicopterSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void SetSpawner(string helicopterSide)
    {
        this.helicopterSide = helicopterSide;
    }

    private IEnumerator Crash()
    {
        // remove health bar
        Destroy(healthBar.gameObject);
        gameObject.tag = ObjectTags.INDESTRUCTABLE_OBSTACLE;
        isHovering = false;

        // rotate the helicopter
        isRotating = true;
        yield return new WaitForSeconds(1f);
        isRotating = false;

        // send the police car down the screen as an obstacle
        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = crashSpeed * -transform.up;
        spawner.RemoveHelicopter(helicopterSide);
    }

    private IEnumerator DamageMarker()
    {
        float xOffset = UnityEngine.Random.Range(-0.2f, 0.2f);
        float yOffset = UnityEngine.Random.Range(-0.9f, 0.85f);
        GameObject marker = Instantiate(damageInidcator, new Vector3(this.transform.position.x + xOffset, this.transform.position.y + yOffset, -1), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(0.25f);
        Destroy(marker);
    }
}
