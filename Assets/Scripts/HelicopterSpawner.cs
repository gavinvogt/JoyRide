using System.Collections;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject helicopterPrefab;
    [SerializeField] private float minSpawnWaitTime;
    [SerializeField] private float spawnMaxTime;
    [SerializeField] private float spawnRangeY;
    private float timeBeforeNextSpawn;

    // Track how many helicopters exist
    private bool[] helicopterExists = { false, false };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeNextSpawn -= Time.deltaTime;
        Debug.Log("next spawn in " + timeBeforeNextSpawn);
        if (timeBeforeNextSpawn <= 0)
        {
            Debug.Log("did spawn");
            SpawnHelicopter();
            ResetSpawnTimer();
        }
    }

    private void SpawnHelicopter()
    {
        int helicopterSide = ChooseHelicopterSide();
        Transform spawnTransform = spawners[helicopterSide];
        GameObject helicopter = Instantiate(
            helicopterPrefab,
            new Vector3(spawnTransform.position.x, spawnTransform.position.y + Random.Range(-spawnRangeY / 2, spawnRangeY / 2)),
            spawnTransform.rotation
        );
        helicopterExists[helicopterSide] = true;
        helicopter.SendMessage("SetSpawner", this, SendMessageOptions.RequireReceiver);
        helicopter.SendMessage(
            "SetHelicopterSide",
            (helicopterSide == 0) ? "left" : "right",
            SendMessageOptions.RequireReceiver
        );
    }

    private void ResetSpawnTimer()
    {
        timeBeforeNextSpawn = minSpawnWaitTime + Random.Range(0, spawnMaxTime);
    }

    public int ChooseHelicopterSide()
    {
        if (helicopterExists[0])
        {
            return 1;
        }
        else if (helicopterExists[1])
        {
            return 0;
        }
        else
        {
            return Random.Range(0, 2);
        }
    }

    public void RemoveHelicopter(string side)
    {
        if (side == "left")
        {
            helicopterExists[0] = false;
        }
        else
        {
            helicopterExists[1] = false;
        }
    }
}
