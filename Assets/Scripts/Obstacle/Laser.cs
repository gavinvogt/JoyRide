using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Laser : MonoBehaviour
{
    private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] protected AudioClip laserSound;
    private AudioSource laserAudioSource = null;
    private LaserSpawner laserSpawner;
    void Start()
    {
        health = 10;
        healthBar.SetMaxHealth(health);
        laserSpawner = GameObject.Find("Laser Spawners").GetComponent<LaserSpawner>();
    }

    public IEnumerator Spawn()
    {
        laserAudioSource = SoundFXManager.instance.LoopSoundFXClip(laserSound, transform, 1f);
        yield return new WaitForSeconds(1f);
        GameObject laser = gameObject.transform.GetChild(2).gameObject;
        laser.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<Animator>().SetBool("Active", true);
        laser.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DecreaseHealth(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
        if (health <= 0)
        {
            StopAllCoroutines();
            laserSpawner.DecreaseLaser();
            GameScore.instance.IncrementLasersDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (laserAudioSource != null)
        {
            Destroy(laserAudioSource.gameObject);
        }
    }
}
