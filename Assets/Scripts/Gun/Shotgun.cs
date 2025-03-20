using System.Collections;
using System.Linq;
using UnityEngine;

public class Shotgun : Gun
{
    // configure how the shotgun sprays bullets
    [SerializeField] private float bulletSpreadFactorHorizontal;
    [SerializeField] private float bulletSpreadFactorAngle;
    private int bulletsPerShot;
    [SerializeField] private float bulletLifeSpan;
    private bool canFire = true;

    override public IEnumerator Fire()
    {
        if (canFire)
        {
            if (car.GetIsShielded() && car.GetCurrentAmmo() > 1)
            {
                int numBulletsToShoot = Mathf.RoundToInt(bulletsPerShot * 0.75f);
                Shoot(numBulletsToShoot);
                StartCoroutine(StartShootCooldown());
                yield return new WaitForSeconds(fireCooldown / 5.0f);
                Shoot(numBulletsToShoot);
            }
            else if (car.GetCurrentAmmo() > 0)
            {
                Shoot(bulletsPerShot);
                StartCoroutine(StartShootCooldown());
            }
            yield return null;
        }
    }

    private void Shoot(int numBullets)
    {
        canFire = false;
        car.UseAmmo();
        player.GetComponent<Player>().UpdatePlayerUI();

        // Fire the bullets
        SoundFXManager.instance.PlaySoundFXClip(shotSound, transform, 0.6f);
        var bullets = Enumerable.Range(1, numBullets).Select(
            _ => Instantiate(bulletPrefab, GetBulletStartPosition(), GetBulletAngle())
        ).ToArray();
        foreach(GameObject bullet in bullets)
        {
            bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
        }
        StartCoroutine(DestroyBullets(bullets));
    }

    private IEnumerator StartShootCooldown()
    {
        // Allow firing after cooldown
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
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

    public void SetBulletCount(int numBullets)
    {
        bulletsPerShot = numBullets;
    }
}
