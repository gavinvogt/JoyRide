using System.Collections;
using System.Linq;
using UnityEngine;

public class Shotgun : Gun
{
    // configure how the shotgun sprays bullets
    [SerializeField] private float bulletSpreadFactorHorizontal;
    [SerializeField] private float bulletSpreadFactorAngle;
    [SerializeField] private int bulletsPerShot;
    [SerializeField] private float bulletLifeSpan;

    override public IEnumerator Fire()
    {
        if (car.getCurrentAmmo() > 0)
        {
            car.useAmmo();
            player.GetComponent<Player>().updatePlayerUI();

            // Fire the bullets
            var bullets = Enumerable.Range(1, bulletsPerShot).Select(
                _ => Instantiate(bulletPrefab, GetBulletStartPosition(), GetBulletAngle())
            ).ToArray();
            StartCoroutine(DestroyBullets(bullets));

            // Continue firing after cooldown
            yield return new WaitForSeconds(fireCooldown);
            if (Input.GetButton("Fire1"))
            {
                // Continue firing
                StartCoroutine(Fire());
            }
        }
    }

    private Vector2 GetBulletStartPosition()
    {
        return firePoint.position + Vector3.right * Random.Range(
            -bulletSpreadFactorHorizontal,
            bulletSpreadFactorHorizontal
        );
    }

    private Quaternion GetBulletAngle()
    {
        return firePoint.rotation * Quaternion.AngleAxis(
            Random.Range(-bulletSpreadFactorAngle, bulletSpreadFactorAngle),
            Vector3.forward
        );
    }

    private IEnumerator DestroyBullets(GameObject[] bullets)
    {
        yield return new WaitForSeconds(bulletLifeSpan);
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}
