using UnityEngine;
using System.Collections;
using System;

public class Helicopter : MonoBehaviour, BaseEnemy
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float crashAngle;
    [SerializeField] private float hoverRangeY;

    // track how the helicopter is moving
    private bool isHovering = false;
    [SerializeField] private float hoverPeriod;
    private Vector3 initialPosition;
    private bool isRotating = false;

    // track helicopter health
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;

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
        }
        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Crash());
        }
    }

    private IEnumerator Crash()
    {
        // remove health bar
        Destroy(healthBar.gameObject);
        gameObject.tag = "Obstacle";

        // rotate the helicopter
        isRotating = true;
        transform.eulerAngles = new Vector3(0, 0, crashAngle);
        yield return new WaitForSeconds(1.5f);
        isRotating = false;

        // send the police car down the screen as an obstacle
        // TODO: uncomment and get speed from obstacle spawner
        rb.linearVelocity = new Vector2(0, -25);
        // rb.linearVelocity = new Vector2(0, -1f * os.GetSpeed());
        // os.DecreaseHelicopterCount();
    }
}
