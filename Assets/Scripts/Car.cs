using UnityEngine;

public class Car : MonoBehaviour
{
    public Player player;

    // player jumping out of car to control a new one
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private Transform leftJumpPoint;
    [SerializeField] private Transform rightJumpPoint;
    // moving the gun
    [SerializeField] private GameObject gun;

    private bool isRotating;
    private int rotationSpeed = 60;
    private GameObject rotateTarget;

    [SerializeField] private int drivingSpeed;

    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int maxAmmoCount;
    private int currentAmmoCount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: implement
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        currentAmmoCount = maxAmmoCount;

        rotateTarget = new GameObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRotating == true)
        {
            float step = rotationSpeed * (rotateTarget.transform.eulerAngles.z / rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget.transform.rotation, step);
        }
    }

    public void RotateCar(string direction)
    {
        if (direction == "left")
        {
            rotateTarget.transform.eulerAngles = new Vector3(0, 0, Random.Range(-91, -60));
        } else
        {
            rotateTarget.transform.eulerAngles = new Vector3(0, 0, Random.Range(60, 91));
        }
        isRotating = true;
    }

    void ShootPlayerProjectile(string direction)
    {
        if (currentAmmoCount > 0) {
            currentAmmoCount--;
            Transform jumpPoint = direction == "left" ? leftJumpPoint : rightJumpPoint;
            GameObject projectile = Instantiate(playerProjectilePrefab, jumpPoint.position, jumpPoint.rotation);

            // transfer player to the projectile
            projectile.SendMessage("SetPlayer", player, SendMessageOptions.RequireReceiver);
            projectile.SendMessage("SetParent", gameObject);
            Reset();
        }
    }

    void SetPlayer(Player player)
    {
        this.player = player;
        gun.SendMessage("SetPlayer", player);
    }

    void Reset()
    {
        player = null;
        gun.SendMessage("Reset");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            currentHealth--;
            if (player != null)
            {
                player.GetComponent<Player>().updatePlayerUI();
            }
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (player != null)
        {
            player.SendMessage("NullCar");
            player = null;
        }
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -5f);
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        if(Random.Range(0,2) < 1)
        {
            RotateCar("left");
        } else
        {
            RotateCar("Right");
        }
    }

    public void DestroyPivot()
    {      
        Destroy(rotateTarget);
        Destroy(this.gameObject);
    }

    public float getHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }

    public float getAmmoPercentage()
    {
        return (float)currentAmmoCount / (float)maxAmmoCount;
    }
    public int getCurrentAmmo()
    {
        return currentAmmoCount;
    }
    public void useAmmo()
    {
        currentAmmoCount--;
    }

    public int getDrivingSpeed()
    {
        return drivingSpeed;
    }
}
