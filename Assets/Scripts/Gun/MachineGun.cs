using System.Collections;
using UnityEngine;

public class MachineGun : Gun
{
    override public IEnumerator Fire()
    {
        if (car.getCurrentAmmo() > 0)
        {
            car.useAmmo();
            player.GetComponent<Player>().updatePlayerUI();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireCooldown);
            if (Input.GetButton("Fire1"))
            {
                // Continue firing
                StartCoroutine(Fire());
            }
        }
    }
}
