using UnityEngine;

public class Car : MonoBehaviour
{
    public Player player;

    // player jumping out of car to control a new one
    [SerializeField] private GameObject playerProjectilePrefab;
    [SerializeField] private Transform leftJumpPoint;
    [SerializeField] private Transform rightJumpPoint;

    // moving the gun
    [SerializeField] private GameObject gun;

    void ShootPlayerProjectile(string direction)
    {
        Transform jumpPoint = direction == "left" ? leftJumpPoint : rightJumpPoint;
        GameObject projectile = Instantiate(playerProjectilePrefab, jumpPoint.position, jumpPoint.rotation);

        // transfer player to the projectile
        projectile.SendMessage("SetPlayer", player, SendMessageOptions.RequireReceiver);
        projectile.SendMessage("SetParent", gameObject);
        Reset();
    }

    void SetPlayer(Player player)
    {
        this.player = player;
        gun.SendMessage("SetPlayer", player);
    }

    void Reset()
    {
        player = null;
        gun.SendMessage("Reset");
    }
}
