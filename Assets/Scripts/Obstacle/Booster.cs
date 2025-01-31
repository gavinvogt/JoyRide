using System.Collections;
using UnityEngine;

public class Booster : Obstacle
{
    [SerializeField] BoosterType boosterType;

    ArrayList carsAlreadyBoosted;

    public enum BoosterType
    {
        HEALTH,
        AMMO,
        HANDLING,
        GOONS
    }

    public override void Spawn()
    {
        carsAlreadyBoosted = new ArrayList();
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
            if (!CarAlreadyBoosted(tempCar)) {
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

    private bool CarAlreadyBoosted(Car car)
    {
        if (!carsAlreadyBoosted.Contains(car))
        {
            carsAlreadyBoosted.Add(car);
            return false;
        }
        return true;
    }
}
