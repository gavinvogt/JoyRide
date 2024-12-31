using UnityEngine;

public class Booster : Obstacle
{
    [SerializeField] BoosterType boosterType;
    private enum BoosterType
    {
        HEALTH,
        AMMO,
        HANDLING,
        GOONS
    }

    public override void Spawn()
    {
        base.Spawn();
        switch (boosterType)
        {
            case BoosterType.HEALTH:
                boosterType = BoosterType.HEALTH;
                break;
            case BoosterType.AMMO:
                boosterType = BoosterType.AMMO;
                break;
            case BoosterType.HANDLING:
                boosterType = BoosterType.HANDLING;
                break;
            case BoosterType.GOONS:
                boosterType = BoosterType.GOONS;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            Car tempCar = collision.gameObject.GetComponent<Car>();
            switch (boosterType)
            {
                case BoosterType.HEALTH:
                    tempCar.AddHealth();
                    break;
                case BoosterType.AMMO:
                    tempCar.MaximizeAmmo();
                    break;
                case BoosterType.HANDLING:
                    tempCar.StartCoroutine(tempCar.BoostSpeed());
                    break;
                case BoosterType.GOONS:
                    tempCar.SpawnNPCs();
                    break;
            }
        }
    }
}
