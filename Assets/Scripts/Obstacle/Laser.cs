using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Laser : MonoBehaviour
{
    private int health;
    [SerializeField] private HealthBar healthBar;
    private LaserSpawner laserSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 10;
        healthBar.SetMaxHealth(health);
        laserSpawner = GameObject.Find("Laser Spawners").GetComponent<LaserSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        GameObject laser = gameObject.transform.GetChild(2).gameObject;
        laser.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        laser.transform.localScale = new Vector3(11.5f, .5f, 1f);
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
            Destroy(this.gameObject);
        }
    }
}
