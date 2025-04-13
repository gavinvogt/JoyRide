public class CarProperties
{
    public static readonly CarProperties SPORTS_CAR = new(
        baseStats: new CarStats(
            speed: 8.5,
            health: 15,
            ammo: 125,
            damage: 1.75,
            attackRate: 0.2,
            spread: 7.5
        ),
        statsPerLevel: new CarStats(
            speed: 0.5,
            health: 2,
            ammo: 15,
            damage: 0.25,
            attackRate: -0.02,
            spread: -1
        )
    );
    public static readonly CarProperties TANK = new(
        baseStats: new CarStats(
            speed: 3.5,
            health: 30,
            ammo: 15,
            damage: 15,
            attackRate: 1.25,
            spread: 3
        ),
        statsPerLevel: new CarStats(
            speed: 0.3,
            health: 5,
            ammo: 2,
            damage: 2,
            attackRate: -0.05,
            spread: -0.6
        )
    );
    public static readonly CarProperties TRUCK = new(
        new CarStats(
            speed: 7,
            health: 25,
            ammo: 20,
            damage: 10,
            bulletsPerShot: 2,
            attackRate: 0.9,
            spread: 25
        ),
        statsPerLevel: new CarStats(
            speed: 0.4,
            health: 3,
            ammo: 4,
            bulletsPerShot: 1,
            damage: 0,
            attackRate: -0.05,
            spread: -2
        )
    );

    public static IEnumerable<CarProperties> Values {
        get {
            yield return SPORTS_CAR;
            yield return TANK;
            yield return TRUCK;
        }
    }

    public CarStats BaseStats { get; init; }
    public CarStats StatsPerLevel { get; init; }

    public CarProperties(CarStats baseStats, CarStats statsPerLevel)
    {
        BaseStats = baseStats;
        StatsPerLevel = statsPerLevel;
    }
}
