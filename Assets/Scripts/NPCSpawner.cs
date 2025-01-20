using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;
    [SerializeField] private List<GameObject> NPCSpawners;
    [SerializeField] private List<GameObject> NPCPrefabs;

    private int currentRandomFlag;
    private int numCars;

    [SerializeField] private int maxNumCars;

    private void Awake()
    {
        currentRandomFlag = 3;
        numCars = 0;
        spawnTimer = maxSpawnTimer;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer < 0)
        {
            bool carSpawned = false;
            foreach (GameObject spawner in NPCSpawners)
            {
                if (Random.Range(0, 10) < currentRandomFlag && numCars < maxNumCars)
                {
                    numCars++;
                    StartCoroutine(SpawnNPC(spawner, NPCPrefabs[Random.Range(0, NPCPrefabs.Count)]));
                    carSpawned = true;
                    currentRandomFlag = 3;
                }
            }
            if (carSpawned == false)
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

    public IEnumerator SpawnNPC(GameObject spawner, GameObject NPC)
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        Debug.Log("Spawner: " + spawner + " | Position: " + spawner.transform.position);
        GameObject tempNPC = Instantiate(NPC, spawner.transform.position, spawner.transform.rotation, spawner.transform);
        tempNPC.GetComponent<CarNPC>().Spawn();
    }

    public void IncreaseNPC()
    {
        numCars++;
    }

    public void DecreaseNPC()
    {
        numCars--;
    }

    public List<GameObject> GetSpawners()
    {
        return NPCSpawners;
    }

    public List<GameObject> GetPrefabs()
    {
        return NPCPrefabs;
    }
}
