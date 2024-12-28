using UnityEngine;

public class Car : MonoBehaviour
{
    public Player player;

    // player jumping out of car to control a new one
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private Transform leftJumpPoint;
    [SerializeField] private Transform rightJumpPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: implement
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: implement
    }

    void ShootPlayerProjectile(string direction)
    {
        var jumpPoint = direction == "left" ? leftJumpPoint : rightJumpPoint;
        Instantiate(playerProjectilePrefab, jumpPoint.position, jumpPoint.rotation);
    }
}
