using System.Collections;
using UnityEngine;

public class RocketGun : Gun
{
    // configure how the rocket launcher shoots ... rockets
    [SerializeField] private float rocketSpreadFactorAngle;
    private bool canFire = true;

    override public IEnumerator Fire()
    {
        if (canFire && car.GetCurrentAmmo() > 0)
        {
            canFire = false;
            car.UseAmmo();
            player.GetComponent<Player>().UpdatePlayerUI();

            // Fire the rocket
            SoundFXManager.instance.PlaySoundFXClip(shotSound, transform, 0.55f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, GetBulletAngle());
            bullet.GetComponent<Tank_Bullet>().SetDamage(bulletDamage);

            // Allow firing after cooldown
            yield return new WaitForSeconds(fireCooldown);
            canFire = true;
        }
    }

    private Quaternion GetBulletAngle()
    {
        return firePoint.rotation * Quaternion.AngleAxis(
            Random.Range(-rocketSpreadFactorAngle, rocketSpreadFactorAngle),
            Vector3.forward
        );
    }
}
