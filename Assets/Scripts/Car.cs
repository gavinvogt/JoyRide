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

    private bool isRotating;
    private int rotationSpeed = 60;
    private GameObject rotateTarget;

    [SerializeField] private int drivingSpeed;

    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int maxAmmoCount;
    private int currentAmmoCount;

    private NPCSpawner npcs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null) OverrideCursor();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        currentAmmoCount = maxAmmoCount;

        rotateTarget = new GameObject();
        npcs = GameObject.Find("NPC Spawner").GetComponent<NPCSpawner>();
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

        // transfer player to the projectile
        projectile.SendMessage("SetPlayer", player, SendMessageOptions.RequireReceiver);
        projectile.SendMessage("SetParent", gameObject);
        Reset();
    }

    void SetPlayer(Player player)
    {
        this.player = player;
        gun.SendMessage("SetPlayer", player);
        if (player != null) OverrideCursor();
    }

    void Reset()
    {
        player = null;
        gun.SendMessage("Reset");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ObjectTags.IsObstacle(collision.gameObject.tag))
        {
            currentHealth--;
            if (ObjectTags.IsDestructableObstacle(collision.gameObject.tag)) Destroy(collision.gameObject);
            if (player != null)
            {
                StartCoroutine(FlashColor());
                player.GetComponent<Player>().updatePlayerUI();
            }
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (player != null)
        {
            player.SendMessage("NullCar");
        }
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -5f);
        this.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<CarNPC>().enabled = false;
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
            if (player != null)
            {
                player.GetComponent<Player>().updatePlayerUI();
            }
        }
    }

    public void MaximizeAmmo()
    {
        currentAmmoCount = maxAmmoCount;
        if (player != null)
        {
            player.GetComponent<Player>().updatePlayerUI();
        }
    }

    public IEnumerator BoostSpeed()
    {
        drivingSpeed += 2;
        yield return new WaitForSeconds(1.5f);
        drivingSpeed -= 2;
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
            }
        }
    }

    IEnumerator FlashColor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        this.gameObject.transform.GetChild(2).GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        this.gameObject.transform.GetChild(2).GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
