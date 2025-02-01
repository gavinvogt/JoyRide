using UnityEngine;

public class SheidlSpecial : SpecialMoveBase
{
    Car car;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = this.gameObject.GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            
        }
    }

    public override void ActivateSpecialMove()
    {
        if (isActive || isAbilityOnCD) return;



        isActive = true;
    }

    public override void EndSpecialMove()
    {
        
        isActive = false;
        StartAbilityCD();
    }

    public void OnDestroy()
    {
        if (isActive)
        {
            
        }
    }
}
