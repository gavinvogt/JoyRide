using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;
    [SerializeField] private List<GameObject> obstacleSpawners;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    private int currentRandomFlag = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer < 0)
        {
            bool obsSpawned = false;
            int obsCounter = 0;
            foreach (GameObject spawner in obstacleSpawners)
            {
                if (Random.Range(0, 10) <  currentRandomFlag && obsCounter < obstacleSpawners.Count -1)
                {
                    GameObject tempOb = Instantiate(obstaclePrefabs[Random.Range(0,obstaclePrefabs.Count)], spawner.transform);
                    tempOb.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * speed);
                    obsSpawned = true;
                    obsCounter++;
                    currentRandomFlag = 5;
                }
            }
            if(obsSpawned == false)
            {
                currentRandomFlag++;
            }
            spawnTimer = maxSpawnTimer;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
