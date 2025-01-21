using System.Collections;
using UnityEngine;

public class Mine : Obstacle
{
    ArrayList damagedCars;

    void Start()
    {
        damagedCars = new ArrayList();
    }

    public bool CarAlreadyDamaged(Car car)
    {
        if (!damagedCars.Contains(car))
        {
            damagedCars.Add(car);
            return false;
        }
        return true;
    }
}
