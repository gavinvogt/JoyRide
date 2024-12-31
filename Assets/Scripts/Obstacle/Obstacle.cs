using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected ObstacleSpawner os;

    protected virtual void Awake()
    {
        os = GameObject.Find("Highway").GetComponent<ObstacleSpawner>();
    }

    public virtual void Spawn()
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * os.GetSpeed());
    }
}
