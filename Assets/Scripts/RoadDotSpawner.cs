using UnityEngine;
using System.Collections.Generic;

public class RoadDotSpawner : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;
    [SerializeField] private List<GameObject> dotSpawners;
    [SerializeField] private GameObject dotPrefab;
 
    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        maxSpawnTimer = (40 - speed) / 80f;
        if(spawnTimer < 0)
        {
            foreach(GameObject spawner in dotSpawners)
            {
                GameObject tempdot = Instantiate(dotPrefab, spawner.transform);
                tempdot.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * speed);
            }
            spawnTimer = maxSpawnTimer;
        } else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

}
