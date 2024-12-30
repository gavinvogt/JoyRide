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
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        switch (boosterType)
        {
            case BoosterType.HEALTH:
                boosterType = BoosterType.HEALTH;
                renderer.color = new Color(0f, 1f, 0f); //green
                break;
            case BoosterType.AMMO:
                boosterType = BoosterType.AMMO;
                renderer.color = new Color(1f, 165f / 255f, 0f); //orange
                break;
            case BoosterType.HANDLING:
                boosterType = BoosterType.HANDLING;
                renderer.color = new Color(128f / 255f, 0f, 128f / 255f); //purple
                break;
            case BoosterType.GOONS:
                boosterType = BoosterType.GOONS;
                renderer.color = new Color(0f, 0f, 1f); //Blue
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Car")
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
