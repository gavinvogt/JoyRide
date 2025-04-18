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

    public CarStats(float speed, float health, int ammo, float damage, float attackRate, float spread)
        : this(speed, health, ammo, damage, 1, attackRate, spread)
    { }

    public CarStats(float speed, float health, int ammo, float damage, int bulletsPerShot, float attackRate, float spread)
    {
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

    public string GetDamageString()
    {
        if (BulletsPerShot == 1)
        {
            return Damage.ToString();
        }
        else
        {
            return $"{Damage} x {BulletsPerShot}";
        }
    }

    public float GetSpeedByLevel(CarStats statsPerLevel, int level)
    {
        return Speed + (statsPerLevel.Speed * level);
    }

    public float GetHealthByLevel(CarStats statsPerLevel, int level)
    {
        return Health + (statsPerLevel.Health * level);
    }

    public int GetAmmoByLevel(CarStats statsPerLevel, int level)
    {
        return Ammo + (statsPerLevel.Ammo * level);
    }

    public float GetDamageByLevel(CarStats statsPerLevel, int level)
    {
        return Damage + (statsPerLevel.Damage * level);
    }

    public int GetBulletsPerShotByLevel(CarStats statsPerLevel, int level)
    {
        return BulletsPerShot + (statsPerLevel.BulletsPerShot * level);
    }

    public float GetAttackRateByLevel(CarStats statsPerLevel, int level)
    {
        return AttackRate + (statsPerLevel.AttackRate * level);
    }

    public float GetSpreadByLevel(CarStats statsPerLevel, int level)
    {
        return Spread + (statsPerLevel.Spread * level);
    }
}
