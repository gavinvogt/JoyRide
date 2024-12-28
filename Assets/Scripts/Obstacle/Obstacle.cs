using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected ObstacleSpawner os;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Awake()
    {
        os = GameObject.Find("Highway").GetComponent<ObstacleSpawner>();
    }

    public virtual void Spawn() 
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * os.GetSpeed());
    }
}
