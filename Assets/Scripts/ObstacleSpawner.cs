using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;
    [SerializeField] private List<GameObject> obstacleSpawners;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private List<GameObject> EnemyPrefabs;
    private int currentRandomFlag = 3;
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
                    StartCoroutine(BlinkWarning(spawner, 0));
                    StartCoroutine(SpawnObstacle(spawner));
                    obsSpawned = true;
                    obsCounter++;
                    currentRandomFlag = 3;
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

    IEnumerator BlinkWarning(GameObject spawner, int numBlinks)
    {
        spawner.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        spawner.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.5f);
        numBlinks++;
        if (numBlinks < 3)
        {
            StartCoroutine(BlinkWarning(spawner,numBlinks));
        }
    }
    IEnumerator SpawnObstacle(GameObject spawner)
    {
        yield return new WaitForSeconds(3f);
        GameObject tempOb = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], spawner.transform);
        tempOb.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * speed);
        /*if(tempOb.name.Contains("Police Car"))
        {
            tempOb.GetComponent<PoliceCar>().Spawn();
        }*/
    }
}
