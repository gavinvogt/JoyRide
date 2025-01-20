using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class Car : MonoBehaviour
{
    public Player player;

    // player jumping out of car to control a new one
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private Transform leftJumpPoint;
    [SerializeField] private Transform rightJumpPoint;
    // moving the gun
    [SerializeField] private GameObject gun;
    // gun cursor
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private GameObject currentCarIndicator;

    private bool isRotating;
    private int rotationSpeed = 60;
    private GameObject rotateTarget;

    [SerializeField] private int drivingSpeed;
    private bool hasSpeedBoost = false;

    [SerializeField] private int maxHealth;
    private int _currentHealth;
    private int currentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (healthBar != null) healthBar.SetHealth(value);
            if (player != null)
            {
                player.GetComponent<Player>().UpdatePlayerUI();
            }
            _currentHealth = value;
        }
    }
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxAmmoCount;
    private int currentAmmoCount;

    [SerializeField] private AudioClip playerLoseClip;

    private NPCSpawner npcs;
    private UI UIScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null) OverrideCursor();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        currentAmmoCount = maxAmmoCount;
        healthBar.SetMaxHealth(maxHealth);

        rotateTarget = new GameObject();
        npcs = GameObject.Find("NPC Spawner").GetComponent<NPCSpawner>();
        UIScript = GameObject.Find("PlayerUI").GetComponent<UI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotating == true)
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
        }
        else
        {
            rotateTarget.transform.eulerAngles = new Vector3(0, 0, Random.Range(60, 91));
        }
        isRotating = true;
    }

    void ShootPlayerProjectile(string direction)
    {
        Transform jumpPoint = direction == "left" ? leftJumpPoint : rightJumpPoint;
        GameObject projectile = Instantiate(playerProjectilePrefab, jumpPoint.position, jumpPoint.rotation);

        UIScript.DisableBoostUI();
        currentCarIndicator.SetActive(false);

        // transfer player to the projectile
        projectile.SendMessage("SetPlayer", player, SendMessageOptions.RequireReceiver);
        projectile.SendMessage("SetParent", gameObject);
        Reset();
    }

    void SetPlayer(Player player)
    {
        this.player = player;
        gun.SendMessage("SetPlayer", player);
        currentCarIndicator.SetActive(true);
        if (hasSpeedBoost)
            UIScript.EnableBoostUI();
        if (player != null) OverrideCursor();
    }

    /// <summary>
    /// Resets the Car and any attached components (e.g. Gun) when the player leaves it.
    /// DO NOT call this method when the player dies, or the game will not know that the player
    /// was in the car that is being destroyed, failing to recognize game over.
    /// </summary>
    void Reset()
    {
        player = null;
        gun.SendMessage("Reset");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ObjectTags.IsObstacle(collision.gameObject.tag))
        {
            if (collision.gameObject.name.Contains("explosion"))
            {
                collision.gameObject.GetComponent<Missile_Explosion>().disableCollider();
                TakeDamage();
            }
            else if (ObjectTags.IsBlockingObstacle(collision.gameObject.tag))
            {
                if (collision.gameObject.name.Contains("Police Car"))
                {
                    if (!collision.gameObject.GetComponent<PoliceCar>().CarAlreadyDamaged(this)) { 
                        TakeDamage();
                    }
                }
                else
                {
                    TakeDamage();
                }
            }
            else if (ObjectTags.IsDestructableObstacle(collision.gameObject.tag) && !collision.gameObject.name.Contains("Missile"))
            {
                Destroy(collision.gameObject);
                TakeDamage();
            }
        }
    }

    private void Die()
    {
        if (player != null)
        {
            // Actions specifically for if a player was in this car (losing game)
            player.SendMessage("NullCar");
            gun.SendMessage("Reset");
            SoundFXManager.instance.PlaySoundFXClip(playerLoseClip, transform, 1f);
        }
        if (healthBar != null) Destroy(healthBar.gameObject);
        healthBar = null;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -5f);
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<CarNPC>().enabled = false;
        if (Random.Range(0, 2) < 1)
        {
            RotateCar("left");
        }
        else
        {
            RotateCar("Right");
        }
    }

    public void DestroyPivot()
    {
        if (player != null)
        {
            SceneManager.LoadScene(sceneName: "EndScreen");
        }
        npcs.DecreaseNPC();
        Destroy(rotateTarget);
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }

    public float GetAmmoPercentage()
    {
        return (float)currentAmmoCount / (float)maxAmmoCount;
    }
    public int GetCurrentAmmo()
    {
        return currentAmmoCount;
    }
    public void UseAmmo()
    {
        currentAmmoCount--;
    }

    public int GetDrivingSpeed()
    {
        return drivingSpeed;
    }

    private void OverrideCursor()
    {
        var cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        StartCoroutine(FlashColor());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void MaximizeAmmo()
    {
        currentAmmoCount = maxAmmoCount;
        if (player != null)
        {
            player.GetComponent<Player>().UpdatePlayerUI();
        }
    }

    public IEnumerator BoostSpeed()
    {
        drivingSpeed += 4;
        hasSpeedBoost = true;
        if (player != null)
        {
            UIScript.EnableBoostUI();
        }
        yield return new WaitForSeconds(2.5f);
        drivingSpeed -= 4;
        hasSpeedBoost = false;
        if (player != null)
        {
            UIScript.DisableBoostUI();
        }
    }

    public void SpawnNPCs()
    {
        if (player != null)
        {
            List<GameObject> Spawners = npcs.GetSpawners();
            List<GameObject> NPCPrefabs = npcs.GetPrefabs();
            List<int> spawnpoints = new List<int>();

            int randInt = Random.Range(0, Spawners.Count);
            spawnpoints.Add(randInt);
            while (spawnpoints.Contains(randInt))
            {
                randInt = Random.Range(0, Spawners.Count);
            }
            spawnpoints.Add(randInt);

            foreach (int i in spawnpoints)
            {
                npcs.StartCoroutine(npcs.SpawnNPC(Spawners[i], NPCPrefabs[Random.Range(0, NPCPrefabs.Count)]));
                npcs.IncreaseNPC();
            }
        }
    }

    IEnumerator FlashColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        gameObject.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
