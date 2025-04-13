using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private float health;
    [SerializeField] private HealthBar healthBar1;
    [SerializeField] private HealthBar healthBar2;
    [SerializeField] protected AudioClip laserSound;
    private AudioSource laserAudioSource = null;
    private LaserSpawner laserSpawner;
    private bool isDead = false;

    void Start()
    {
        health = 10;
        healthBar1.SetMaxHealth(health);
        healthBar2.SetMaxHealth(health);
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

    public void DecreaseHealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar1.SetHealth(health);
            healthBar2.SetHealth(health);
        }
        if (health <= 0 && !isDead)
        {
            isDead = true;
            StopAllCoroutines();
            if (laserSpawner) laserSpawner.DecreaseLaser();
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
