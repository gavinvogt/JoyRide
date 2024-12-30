using System.Collections;
using UnityEngine;

public class RocketGun : Gun
{
    // configure how the rocket launcher shoots ... rockets
    [SerializeField] private float rocketSpreadFactorAngle;
    private bool canFire = true;

    override public IEnumerator Fire()
    {
        if (canFire && car.getCurrentAmmo() > 0)
        {
            canFire = false;
            car.useAmmo();
            player.GetComponent<Player>().updatePlayerUI();

            // Fire the rocket
            SoundFXManager.instance.PlaySoundFXClip(shotSound, transform, 1f);
            Instantiate(bulletPrefab, firePoint.position, GetBulletAngle());

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
