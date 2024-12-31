using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> laserSpawners;
    [SerializeField] private GameObject laserPrefab;
    private int laserCount = 0;

    [SerializeField] private float maxSpawnTimer;
    private float currentMaxTimer;
    private float spawnTimer;

    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        currentMaxTimer = maxSpawnTimer;
        spawnTimer = currentMaxTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer < 0)
        {
            int randInt = Random.Range(0, laserSpawners.Count);
            bool spawned = false;
            while(spawned == false && laserCount < laserSpawners.Count)
            {
                if (laserSpawners[randInt].transform.childCount > 0)
                {
                    randInt = Random.Range(0, laserSpawners.Count);
                } else
                {
                    GameObject newLaser = Instantiate(laserPrefab, laserSpawners[randInt].transform);
                    Laser laser = newLaser.GetComponent<Laser>();
                    laser.StartCoroutine(laser.Spawn());
                    laserCount++;
                    spawned = true;
                }
            }
            spawnTimer = currentMaxTimer;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    public void DecreaseLaser()
    {
        laserCount--;
    }

    public void SetMaxSpawn(float percentage)
    {
        currentMaxTimer = maxSpawnTimer * (1f - percentage);
        if (currentMaxTimer < 1f)
            currentMaxTimer = 1f;
    }
}
