using UnityEngine;

public class SheildSpecial : SpecialMoveBase
{
    [SerializeField] private float shieldDuration;
    [SerializeField] private Car car;
    [SerializeField] private GameObject shield;
    private float timeSinceActivated;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timeSinceActivated += Time.unscaledDeltaTime;
            if (timeSinceActivated >= shieldDuration)
            {
                EndSpecialMove();
            }
        }
    }

    public override void ActivateSpecialMove()
    {
        if (isActive || isAbilityOnCD) return;

        isActive = true;
        timeSinceActivated = 0.0f;
        car.SetIsShielded(true);
        shield.SetActive(true);
    }

    public override void EndSpecialMove()
    {
        isActive = false;
        car.SetIsShielded(false);
        shield.SetActive(false);
        StartAbilityCD();
    }

    public void OnDestroy()
    {
        if (isActive)
        {
            
        }
    }
}
