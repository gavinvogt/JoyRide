using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // current car controlled by player, may be null while jumping
    [SerializeField] private GameObject car;
    private Rigidbody2D rb;

    private int maxSpeed;
    private int speed;

    private ObstacleSpawner os;
    private RoadDotSpawner rds;
    private HelicopterSpawner hs;
    private LaserSpawner ls;

    [SerializeField] private GameObject UI;
    private UI UIScript;
    [SerializeField] private UIDocument inGameMenuDocument;
    private float previousTimeScale;
    [SerializeField] private GameObject soundManagerObject;
    private SoundMixerManager soundManager;

    [SerializeField] AudioClip[] jumpAudioClips;

    private void Start()
    {
        UIScript = UI.GetComponent<UI>();
        UpdatePlayerUI();

        soundManager = soundManagerObject.GetComponent<SoundMixerManager>();
        soundManager.SoundStartUp();
    }

    private void Awake()
    {
        Save.LoadFile();
        rb = car.GetComponent<Rigidbody2D>();
        car.SendMessage("SetPlayer", this);
        os = GameObject.Find("Highway").GetComponent<ObstacleSpawner>();
        rds = GameObject.Find("Highway").GetComponent<RoadDotSpawner>();
        ls = GameObject.Find("Highway").GetComponentInChildren<LaserSpawner>();
        hs = GameObject.Find("Highway").GetComponentInChildren<HelicopterSpawner>();
        maxSpeed = 35;
        speed = 5;
        os.SetSpeed(speed);
        rds.SetSpeed(speed);
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(
                (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0), // left/right
                (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)  // up/down
            ).normalized * car.GetComponent<Car>().GetDrivingSpeed();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: ideally remove logic like this throughout the code and encapsulate the Player logic in
        // the InGameMenuState Execute function
        if (Time.timeScale == 0)
        {
            // Return if paused to prevent any key press handling
            return;
        }

        if (car != null)
        {
            // actions that require player to have a car
            bool jumpLeft = Input.GetKeyDown(KeyCode.Q);
            bool jumpRight = Input.GetKeyDown(KeyCode.E);
            if (jumpLeft || jumpRight)
            {
                // Player jumping out of car
                SoundFXManager.instance.PlayRandomSoundFXClip(jumpAudioClips, transform, 1f);
                car.SendMessage("ShootPlayerProjectile", jumpLeft ? "left" : "right");
                car.SendMessage("Die");
                car = null;
                rb = null;
            }
        }
    }

    public void UpdateCar(GameObject newCar)
    {
        // update to new car
        car = newCar;
        rb = newCar.GetComponent<Rigidbody2D>();
        car.SendMessage("SetPlayer", this);
        car.GetComponent<CarNPC>().ActivateCar();
        car.GetComponent<CarNPC>().enabled = false;
        UpdatePlayerUI();
    }

    public GameObject GetCar()
    {
        return car;
    }

    public void NullCar()
    {
        car = null;
        rb = null;
    }

    public float GetSpeedPercentage()
    {
        return (float)(speed) / (float)maxSpeed;
    }

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(10f);
        speed++;
        os.SetSpeed(speed);
        if (speed % 10 == 0)
        {
            os.IncreaseMaxPoliceCars();
        }
        rds.SetSpeed(speed);
        ls.SetMaxSpawn(GetSpeedPercentage());
        hs.SetMinSpawnTime(GetSpeedPercentage());
        UpdatePlayerUI();
        if (speed < maxSpeed)
        {
            StartCoroutine(IncreaseSpeed());
        }
    }
    public void UpdatePlayerUI()
    {
        if (UIScript) UIScript.updateUI(gameObject);
    }

    public int GetSpeed()
    {
        return speed;
    }
}
