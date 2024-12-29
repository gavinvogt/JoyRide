using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    public float fireCooldown;

    public Player player;
    private Camera mainCamera;
    private Car car;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = this.transform.parent.GetComponent<Car>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
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

    private IEnumerator Fire()
    {
        if (car.getCurrentAmmo() > 0) {
            car.useAmmo();
            player.GetComponent<Player>().updatePlayerUI();
            Instantiate(bulletPrefab, firePoint.position + firePoint.forward, firePoint.rotation);
            yield return new WaitForSeconds(fireCooldown);
            if (Input.GetButton("Fire1"))
            {
                // Continue firing
                StartCoroutine(Fire());
            }
        }
    }

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
