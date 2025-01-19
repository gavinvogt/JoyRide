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
    private bool canFire = true;

    override public IEnumerator Fire()
    {
        if (canFire && car.GetCurrentAmmo() > 0)
        {
            canFire = false;
            car.UseAmmo();
            player.GetComponent<Player>().UpdatePlayerUI();

            // Fire the bullets
            SoundFXManager.instance.PlaySoundFXClip(shotSound, transform, 0.6f);
            var bullets = Enumerable.Range(1, bulletsPerShot).Select(
                _ => Instantiate(bulletPrefab, GetBulletStartPosition(), GetBulletAngle())
            ).ToArray();
            StartCoroutine(DestroyBullets(bullets));

            // Allow firing after cooldown
            yield return new WaitForSeconds(fireCooldown);
            canFire = true;
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
