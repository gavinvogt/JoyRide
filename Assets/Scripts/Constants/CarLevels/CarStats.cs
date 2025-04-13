public readonly struct CarStats
{
    public double Speed { get; init; }
    public double Health { get; init; }
    public int Ammo { get; init; }
    public double Damage { get; init; }
    public int BulletsPerShot { get; init; }
    /** Time between each attack, in seconds */
    public double AttackRate { get; init; }
    public double Spread { get; init; }

    public CarStats(double speed, double health, int ammo, double damage, double attackRate, double spread) {
        Speed = speed;
        Health = health;
        Ammo = ammo;
        Damage = damage;
        BulletsPerShot = 1;
        AttackRate = attackRate;
        Spread = spread;
    }

    public CarStats(double speed, double health, int ammo, double damage, int bulletsPerShot, double attackRate, double spread) {
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
