using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected AudioClip shotSound;
    public float fireCooldown;

    public Player player;
    protected Camera mainCamera;
    protected Car car;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = transform.parent.GetComponent<Car>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Time.timeScale != 0)
        {
            // Adjust angle of gun by mouse
            var targetAngle = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rot = Mathf.Atan2(targetAngle.y, targetAngle.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rot - 90, Vector3.forward);

            if (Input.GetButtonDown("Fire1"))
            {
                // Start firing gun
                StartCoroutine(Fire());
            }
        }
    }

    /// <summary>
    /// Coroutine for firing while the Fire1 button is pressed. Should fire, await the cooldown,
    /// and recurse if Fire1 is still pressed.
    /// </summary>
    abstract public IEnumerator Fire();

    void SetPlayer(Player player)
    {
        this.player = player;
    }

    void Reset()
    {
        player = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
