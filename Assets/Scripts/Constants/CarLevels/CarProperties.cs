using System.Collections.Generic;
using static Car;

public class CarProperties
{
    public static readonly CarProperties SPORTS_CAR = new(
        "Sports Car",
        CarType.SPORTS_CAR,
        "Assets/UI/IconBig.png",
        baseStats: new CarStats(
            speed: 8.5f,
            health: 15f,
            ammo: 125,
            damage: 1.75f,
            attackRate: 0.2f,
            spread: 7.5f
        ),
        statsPerLevel: new CarStats(
            speed: 0.5f,
            health: 2f,
            ammo: 15,
            damage: 0.25f,
            attackRate: -0.02f,
            spread: -1f
        )
    );
    public static readonly CarProperties TANK = new(
        "Tank",
        CarType.TANK,
        // TODO: use tank image
        "Assets/UI/IconBig.png",
        baseStats: new CarStats(
            speed: 3.5f,
            health: 30f,
            ammo: 15,
            damage: 15f,
            attackRate: 1.25f,
            spread: 3f
        ),
        statsPerLevel: new CarStats(
            speed: 0.3f,
            health: 5f,
            ammo: 2,
            damage: 2f,
            attackRate: -0.05f,
            spread: -0.6f
        )
    );
    public static readonly CarProperties TRUCK = new(
        "Truck",
        CarType.SHOTGUN_TRUCK,
        // TODO: use truck image
        "Assets/UI/IconBig.png",
        new CarStats(
            speed: 7f,
            health: 25f,
            ammo: 20,
            damage: 2f,
            bulletsPerShot: 10,
            attackRate: 0.9f,
            spread: 25f
        ),
        statsPerLevel: new CarStats(
            speed: 0.4f,
            health: 3f,
            ammo: 4,
            bulletsPerShot: 1,
            damage: 0f,
            attackRate: -0.05f,
            spread: -2f
        )
    );

    public static IEnumerable<CarProperties> Values
    {
        get
        {
            yield return SPORTS_CAR;
            yield return TANK;
            yield return TRUCK;
        }
    }

    public string Name { get; init; }
    public CarType CarType { get; init; }
    public string SmallImage { get; init; }
    public CarStats BaseStats { get; init; }
    public CarStats StatsPerLevel { get; init; }

    public CarProperties(string name, CarType carType, string smallImage, CarStats baseStats, CarStats statsPerLevel)
    {
        Name = name;
        CarType = carType;
        SmallImage = smallImage;
        BaseStats = baseStats;
        StatsPerLevel = statsPerLevel;
    }

    public static CarProperties GetPropertiesByType(CarType type)
    {
        return type switch
        {
            CarType.SPORTS_CAR => SPORTS_CAR,
            CarType.SHOTGUN_TRUCK => TRUCK,
            CarType.TANK => TANK,
            _ => null,
        };
    }

    public override string ToString()
    {
        return $"CarProperties(\"{Name}\")";
    }

    /** Gets this car's stats at the level from saved data */
    public CarStats StatsFromSaveData(SaveData.CarSaveData saveData)
    {
        return new CarStats(
            speed: BaseStats.GetSpeedByLevel(StatsPerLevel, saveData.GetSpeedUpgradeLevel()),
            health: BaseStats.GetHealthByLevel(StatsPerLevel, saveData.GetHealthUpgradeLevel()),
            ammo: BaseStats.GetAmmoByLevel(StatsPerLevel, saveData.GetAmmoUpgradeLevel()),
            damage: BaseStats.GetDamageByLevel(StatsPerLevel, saveData.GetDamageUpgradeLevel()),
            bulletsPerShot: BaseStats.GetBulletsPerShotByLevel(StatsPerLevel, saveData.GetDamageUpgradeLevel()),
            attackRate: BaseStats.GetAttackRateByLevel(StatsPerLevel, saveData.GetAttackRateUpgradeLevel()),
            spread: BaseStats.GetSpreadByLevel(StatsPerLevel, saveData.GetAttackSpreadUpgradeLevel())
        );
    }
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
