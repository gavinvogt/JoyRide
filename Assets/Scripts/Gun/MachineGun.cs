using System.Collections;
using UnityEngine;

public class MachineGun : Gun
{
    // configure machine gun bullet spread
    [SerializeField] private float bulletSpreadFactorAngle;

    private AudioSource blastAudioSource = null;

    override public IEnumerator Fire()
    {
        if (player != null && car.getCurrentAmmo() > 0)
        {
            car.useAmmo();
            if (player) player.GetComponent<Player>().UpdatePlayerUI();
            if (blastAudioSource == null)
            {
                // Start machine gun sound
                blastAudioSource = SoundFXManager.instance.LoopSoundFXClip(shotSound, transform, 1f);
            }

            Instantiate(bulletPrefab, firePoint.position, GetBulletAngle());
            yield return new WaitForSecondsRealtime(fireCooldown);
            if (Input.GetButton("Fire1") && Time.timeScale != 0)
            {
                // Continue firing
                StartCoroutine(Fire());
            }
            else
            {
                // Stop sound effect
                StopShotSound();
            }
        }
        else
        {
            // Player left car or out of ammo, stop shooting
            StopShotSound();
        }
    }

    private Quaternion GetBulletAngle()
    {
        return firePoint.rotation * Quaternion.AngleAxis(
            Random.Range(-bulletSpreadFactorAngle, bulletSpreadFactorAngle),
            Vector3.forward
        );
    }

    private void StopShotSound()
    {
        if (blastAudioSource != null)
        {
            Destroy(blastAudioSource.gameObject);
            blastAudioSource = null;
        }
    }

    private void OnDestroy()
    {
        StopShotSound();
    }
}
