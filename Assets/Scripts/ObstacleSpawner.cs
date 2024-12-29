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
    [SerializeField] private GameObject PolicePrefab;
    private int currentRandomFlag = 3;
    private int policeRandomFlag = 3;
    private int numPoliceCars;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        numPoliceCars = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer < 0)
        {
            bool obsSpawned = false;
            int obsCounter = 0;
            bool policeSpawned = false;
            foreach (GameObject spawner in obstacleSpawners)
            {
                if (Random.Range(0, 10) < currentRandomFlag && obsCounter < obstacleSpawners.Count - 1)
                {
                    StartCoroutine(BlinkWarning(spawner, 0));
                    StartCoroutine(SpawnObstacle(spawner, obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)]));
                    obsSpawned = true;
                    obsCounter++;
                    currentRandomFlag = 3;
                }
                if (Random.Range(0, 10) < policeRandomFlag && numPoliceCars < 2)
                {
                    //StartCoroutine(BlinkWarning(spawner, 0));
                    StartCoroutine(SpawnObstacle(spawner, PolicePrefab));
                    numPoliceCars++;
                    policeSpawned = true;
                    policeRandomFlag = 3;
                }
            }

            if (obsSpawned == false)
            {
                currentRandomFlag++;
            }
            if (policeSpawned == false)
            {
                policeRandomFlag++;
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
    IEnumerator SpawnObstacle(GameObject spawner, GameObject obstacle)
    {
        yield return new WaitForSeconds(3f);
        GameObject tempOb = Instantiate(obstacle, spawner.transform);
        if(tempOb.name.Contains("Road Blockade"))
        {
            tempOb.GetComponent<Mine>().Spawn();
        } else if (tempOb.name.Contains("Booster"))
        {
            tempOb.GetComponent<Booster>().Spawn();
        } else if(tempOb.name.Contains("Police Car"))
        {
            yield return new WaitForSeconds(Random.Range(0f,1f));
            tempOb.GetComponent<PoliceCar>().Spawn();
        }
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void DecreasePoliceCount()
    {
        numPoliceCars--;
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
}
