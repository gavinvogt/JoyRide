using UnityEngine;
using System.Collections;

public class PoliceCar : Obstacle, BaseEnemy
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject roadSpikePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firePointXRange;
    private bool isMoving;
    private GameObject movePoint;
    private int moveSpeed;
    private bool isRotating;
    private bool isInvulnerable;

    private int timesMoved;

    ArrayList damagedCars;

    private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject damageInidcator;
    [SerializeField] private AudioClip carDestroyedClip;

    protected override void Awake()
    {
        base.Awake();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        isMoving = false;
        isRotating = false;
        isInvulnerable = true;

        moveSpeed = 3;
        movePoint = new GameObject();
        movePoint.transform.parent = transform.parent;

        timesMoved = 0;

        damagedCars = new ArrayList();

        health = 40;
        healthBar.SetMaxHealth(health);

        os = GameObject.Find("Highway").GetComponentInChildren<ObstacleSpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving == true && movePoint != null)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, step);
        }
        if (isRotating == true && movePoint != null)
        {
            float step = movePoint.transform.eulerAngles.z * Time.deltaTime;
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
        yield return new WaitForSeconds(.3f);
        isInvulnerable = false;
        yield return new WaitForSeconds(.2f);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(DeployRoadSpikes(0));
    }

    IEnumerator DeployRoadSpikes(int numShot)
    {
        yield return new WaitForSeconds(.5f);
        float xOffset = Random.Range(-firePointXRange / 2, firePointXRange / 2);
        GameObject roadSpikesObject = Instantiate(
            roadSpikePrefab,
            new Vector3(firePoint.position.x + xOffset, firePoint.position.y),
            firePoint.rotation
        );
        float roadSpikeScale = Random.Range(1f, 1.6f);
        roadSpikesObject.transform.localScale = new Vector3(roadSpikeScale, roadSpikeScale, 1);
        roadSpikesObject.transform.parent = null;
        roadSpikesObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * os.GetSpeed());
        numShot++;
        if (numShot < 3)
        {
            StartCoroutine(DeployRoadSpikes(numShot));
        }
        else
        {
            StartCoroutine(Move(3f));
        }
    }

    IEnumerator Move(float seconds)
    {
        float xOffset = Random.Range(-2f, 2f);
        while (xOffset > -1f && xOffset < 1f)
        {
            xOffset = Random.Range(-2f, 2f);
        }
        movePoint.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x + xOffset, -4f, 4f), this.transform.position.y);

        isMoving = true;
        timesMoved++;
        yield return new WaitForSeconds(seconds);
        isMoving = false;
        if (timesMoved < 3)
        {
            StartCoroutine(DeployRoadSpikes(0));
        }
        else
        {
            StartCoroutine(Die(false));
        }
    }

    public void DecreaseHealth(float damage)
    {
        if (isInvulnerable)
            return;
        if (health > 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
            StartCoroutine(DamageMarker());
        }
        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die(true));
        }
    }

    public void DecreaseHealthNoDamageMarker(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die(true));
        }
    }

    IEnumerator Die(bool givePoints)
    {
        // remove health bar
        Destroy(healthBar.gameObject);
        gameObject.tag = ObjectTags.INDESTRUCTABLE_OBSTACLE;
        if (givePoints)
            GameScore.instance.IncrementPoliceCarsDestroyed();
        SoundFXManager.instance.PlaySoundFXClip(carDestroyedClip, transform, 1f);

        // rotate the police car
        isRotating = true;
        movePoint.transform.eulerAngles = new Vector3(0, 0, Random.Range(60, 91));
        yield return new WaitForSeconds(1.5f);
        isRotating = false;

        // send the police car down the screen as an obstacle
        Destroy(movePoint);
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * os.GetSpeed());
        os.DecreasePoliceCount();
    }

    private IEnumerator DamageMarker()
    {
        float xOffset = Random.Range(-0.45f, 0.45f);
        float yOffset = Random.Range(-0.75f, 0.8f);
        GameObject marker = Instantiate(damageInidcator, new Vector3(this.transform.position.x + xOffset, this.transform.position.y + yOffset, -1), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(0.25f);
        Destroy(marker);
    }

    public bool CarAlreadyDamaged (Car car)
    {
        if(!damagedCars.Contains(car)) 
        {
            damagedCars.Add(car);
            StartCoroutine(RemoveDamagedCar(car));
            return false;
        }
        return true;
    }

    private IEnumerator RemoveDamagedCar(Car car)
    {
        yield return new WaitForSeconds(1f);
        damagedCars.Remove(car);
    }
}
