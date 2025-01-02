using UnityEngine;
using System.Collections;
using System;

public class Helicopter : MonoBehaviour, BaseEnemy
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float crashAngle;
    [SerializeField] private float crashSpeed;
    [SerializeField] private float hoverRangeY;

    private float attackCD = 0.75f;
    private bool attackOnCD = false;

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

    [SerializeField] private GameObject spotlightPrefab;
    [SerializeField] private GameObject spotlight;

    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private AudioClip helicopterDestroyedClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.SetMaxHealth(health);
        StartHovering();
        StartCoroutine(SpawnSpotlight());
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
            GameScore.instance.IncrementHelicoptersDestroyed();
            spotlight.GetComponent<Spotlight>().Despawn();
            StartCoroutine(Crash());
        }
    }

    public void SetSpawner(HelicopterSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void SetHelicopterSide(string helicopterSide)
    {
        this.helicopterSide = helicopterSide;
    }

    private IEnumerator Crash()
    {
        // remove health bar
        Destroy(healthBar.gameObject);
        SoundFXManager.instance.PlaySoundFXClip(helicopterDestroyedClip, transform, 1f);
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

    private IEnumerator SpawnSpotlight()
    {
        yield return new WaitForSeconds(1f);
        spotlight = Instantiate(spotlightPrefab, gameObject.transform);
        spotlight.GetComponent<Spotlight>().Spawn(this);
        spotlight.transform.parent = null;
    }

    private IEnumerator startAttackCD()
    {
        attackOnCD = true;
        yield return new WaitForSeconds(attackCD);
        attackOnCD = false;
    }

    public void ShootMissile()
    {
        if (!attackOnCD)
        {
            StartCoroutine(startAttackCD());
            GameObject missile = Instantiate(missilePrefab, gameObject.transform);
            missile.transform.parent = null;
            missile.GetComponent<Missile>().SetReferences(this.spotlight.transform, spotlight.GetComponent<Spotlight>());
            missile.GetComponent<CircleCollider2D>().enabled = false;
            Vector3 targ = spotlight.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x -= objectPos.x;
            targ.y -= objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            missile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            missile.GetComponent<Rigidbody2D>().linearVelocity = missile.transform.up * 7.5f;
        }
    }
}
