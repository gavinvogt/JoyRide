using UnityEngine;

public class Car : MonoBehaviour
{
    public Player player;

    // player jumping out of car to control a new one
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private Transform leftJumpPoint;
    [SerializeField] private Transform rightJumpPoint;
    private bool isRotating;
    private int rotationSpeed = 60;
    private GameObject rotateTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: implement
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRotating == true)
        {
            float step = rotationSpeed * (rotateTarget.transform.eulerAngles.z / rotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget.transform.rotation, step);
        }
    }

    public void RotateCar(string direction)
    {
        rotateTarget = new GameObject();
        if (direction == "left")
        {
            rotateTarget.transform.eulerAngles = new Vector3(0, 0, Random.Range(-91, -60));
        } else
        {
            rotateTarget.transform.eulerAngles = new Vector3(0, 0, Random.Range(60, 91));
        }
        isRotating = true;
    }

    void ShootPlayerProjectile(string direction)
    {
        Transform jumpPoint = direction == "left" ? leftJumpPoint : rightJumpPoint;
        GameObject projectile = Instantiate(playerProjectilePrefab, jumpPoint.position, jumpPoint.rotation);

        // transfer player to the projectile
        projectile.SendMessage("SetPlayer", player, SendMessageOptions.RequireReceiver);
        player = null;
    }

    void SetPlayer(Player player)
    {
        this.player = player;
    }
}
