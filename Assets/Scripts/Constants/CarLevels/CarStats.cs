public readonly struct CarStats
{
    public float Speed { get; init; }
    public float Health { get; init; }
    public int Ammo { get; init; }
    public float Damage { get; init; }
    public int BulletsPerShot { get; init; }
    /** Time between each attack, in seconds */
    public float AttackRate { get; init; }
    public float Spread { get; init; }

    public CarStats(float speed, float health, int ammo, float damage, float attackRate, float spread) {
        Speed = speed;
        Health = health;
        Ammo = ammo;
        Damage = damage;
        BulletsPerShot = 1;
        AttackRate = attackRate;
        Spread = spread;
    }

    public CarStats(float speed, float health, int ammo, float damage, int bulletsPerShot, float attackRate, float spread) {
        Speed = speed;
        Health = health;
        Ammo = ammo;
        Damage = damage;
        BulletsPerShot = bulletsPerShot;
        AttackRate = attackRate;
        Spread = spread;
    }

    public override string ToString() =>
        $"(Speed={Speed}, Health={Health}, Ammo={Ammo}, Damage={Damage}, BulletsPerShot={BulletsPerShot}, AttackRate={AttackRate}), Spread={Spread}";
}
