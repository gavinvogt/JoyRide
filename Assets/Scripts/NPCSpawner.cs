using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;
    [SerializeField] private List<GameObject> NPCSpawners;
    [SerializeField] private List<GameObject> NPCPrefabs;
    List<GameObject> spawnableCars;

    private int currentRandomFlag;
    private int numCars;

    [SerializeField] private int maxNumCars;

    private void Awake()
    {
        currentRandomFlag = 3;
        numCars = 0;
        spawnTimer = maxSpawnTimer;

        spawnableCars = new List<GameObject>();
        foreach (GameObject prefab in NPCPrefabs)
        {
            foreach(SaveData.CarSaveData unlockedCar in Save.globalSaveData.carsUnlocked)
            {
                if (prefab.gameObject.name.Contains(unlockedCar.GetName()))
                {
                    Debug.Log(prefab + " | " + unlockedCar.GetName());
                    spawnableCars.Add(prefab);
                }
            }
        }
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
                    StartCoroutine(SpawnNPC(spawner, spawnableCars[Random.Range(0, spawnableCars.Count)]));
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

    public List<GameObject> GetSpawnablePrefabs()
    {
        return spawnableCars;
    }
}
