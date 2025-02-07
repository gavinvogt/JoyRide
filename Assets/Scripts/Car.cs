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

    [SerializeField] private float drivingSpeed;
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
    [SerializeField] private AbilityBar abilityBar;
    [SerializeField] private int maxAmmoCount;
    private int currentAmmoCount;

    [SerializeField] private AudioClip playerLoseClip;

    private NPCSpawner npcs;
    private UI UIScript;
    private bool immuneToDamage;
    private bool isShielded = false;
    private bool isCarDead = false;

    // Special attack
    [SerializeField] private SpecialMoveBase specialMoveScript;
    private int RIGHT_CLICK = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            OverrideCursor();
            SetAbilityCD();
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        currentAmmoCount = maxAmmoCount;
        healthBar.SetMaxHealth(maxHealth);
        abilityBar.SetInitialAbilityCD(specialMoveScript.GetTotalAbilityCD(), specialMoveScript.GetAbilityCDOnEntrance());

        rotateTarget = new GameObject();
        npcs = GameObject.Find("NPC Spawner").GetComponent<NPCSpawner>();
        UIScript = GameObject.Find("PlayerUI").GetComponent<UI>();
    }

    private void Update()
    {
        if (player != null && !isCarDead && Input.GetMouseButtonUp(RIGHT_CLICK))
        {
            ActivateSpecial();
        }
    }

    private void FixedUpdate()
    {
        if (isRotating == true)
        {
            float step = rotationSpeed * (rotateTarget.transform.eulerAngles.z / rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget.transform.rotation, step);
        }
        if (player != null)
        {
            abilityBar.SetCurrentAbilityCD(specialMoveScript.GetTimeLeftOnAbilityCD());
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
        if (ObjectTags.IsObstacle(collision.gameObject.tag) && !immuneToDamage)
        {
            if (isShielded)
            {
                EndAbility();
                return;
            }
            if (collision.gameObject.name.Contains("explosion"))
            {
                collision.gameObject.GetComponent<Missile_Explosion>().disableCollider();
                TakeDamage();
            }
            else if (ObjectTags.IsBlockingObstacle(collision.gameObject.tag))
            {
                if (collision.gameObject.name.Contains("Police Car"))
                {
                    if (!collision.gameObject.GetComponent<PoliceCar>().CarAlreadyDamaged(this))
                    {
                        TakeDamage();
                    }
                }
                else if (collision.gameObject.name.Contains("Helicopter"))
                {
                    if (!collision.gameObject.GetComponent<Helicopter>().CarAlreadyDamaged(this))
                    {
                        TakeDamage();
                    }
                }
                else if (collision.gameObject.name.Contains("Road Blockade"))
                {
                    if (!collision.gameObject.GetComponent<Mine>().CarAlreadyDamaged(this))
                    {
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
        isCarDead = true;
        if (player != null)
        {
            PlayerLoseControl();
        }
        if (healthBar != null) Destroy(healthBar.gameObject);
        if (abilityBar != null) Destroy(abilityBar.gameObject);
        healthBar = null;
        abilityBar = null;
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


    // Actions specifically for if a player was in this car (losing game)
    public void PlayerLoseControl()
    {
        player.SendMessage("NullCar");
        gun.SendMessage("Reset");
        SoundFXManager.instance.PlaySoundFXClip(playerLoseClip, transform, 1f);
    }

    public void DestroyPivot()
    {
        if (player != null)
        {
            SceneManager.LoadScene(sceneName: GameScenes.EndScreen);
        }
        npcs.DecreaseNPC();
        Destroy(rotateTarget);
        Destroy(gameObject);
    }

    public bool IsCarDead()
    {
        return isCarDead;
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

    public float GetDrivingSpeed()
    {
        return drivingSpeed;
    }

    public void SetDrivingSpeed(float speed)
    {
        drivingSpeed = speed;
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

    public bool GetImmuneToDamage()
    {
        return immuneToDamage;
    }

    public void SetImmuneToDamage(bool immune)
    {
        immuneToDamage = immune;
    }

    public bool GetIsShielded()
    {
        return isShielded;
    }

    public void SetIsShielded(bool shielded)
    {
        isShielded = shielded;
    }

    public void EndAbility()
    {
        specialMoveScript.EndSpecialMove();
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

    public void SetAbilityCD()
    {
        specialMoveScript.SetTimeLeftOnAbilityCD(specialMoveScript.GetAbilityCDOnEntrance());
    }

    private void ActivateSpecial()
    {
        specialMoveScript.ActivateSpecialMove();
    }
}
