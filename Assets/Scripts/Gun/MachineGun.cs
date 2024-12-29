using System.Collections;
using UnityEngine;

public class MachineGun : Gun
{
    // configure machine gun bullet spread
    [SerializeField] private float bulletSpreadFactorAngle;

    override public IEnumerator Fire()
    {
        if (car.getCurrentAmmo() > 0)
        {
            car.useAmmo();
            player.GetComponent<Player>().updatePlayerUI();
            Instantiate(bulletPrefab, firePoint.position, GetBulletAngle());
            yield return new WaitForSeconds(fireCooldown);
            if (Input.GetButton("Fire1"))
            {
                // Continue firing
                StartCoroutine(Fire());
            }
        }
    }

    private Quaternion GetBulletAngle()
    {
        return firePoint.rotation * Quaternion.AngleAxis(
            Random.Range(-bulletSpreadFactorAngle, bulletSpreadFactorAngle),
            Vector3.forward
        );
    }
}
