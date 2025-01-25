using UnityEngine;

public abstract class SpecialMoveBase : MonoBehaviour
{
    [SerializeField] protected float abilityCooldown;
    [SerializeField] protected float abilityCooldownOnEntrance;
    protected bool isAbilityOnCD = false;
    private float timeRemainingOnCD;

    protected bool isActive = false;

    public void FixedUpdate()
    {
        if (isAbilityOnCD && !isActive)
        {
            timeRemainingOnCD -= Time.deltaTime;
            if(timeRemainingOnCD <= 0)
            {
                timeRemainingOnCD = 0f;
                isAbilityOnCD = false;
            }
        }
    }

    /// <summary>
    /// Activates the car's special move
    /// </summary>
    public abstract void ActivateSpecialMove();

    /// <summary>
    /// Ends the car's special move
    /// </summary>
    public abstract void EndSpecialMove();

    protected void StartAbilityCD()
    {
        timeRemainingOnCD = abilityCooldown;
        isAbilityOnCD = true;
    }

    public void SetTimeLeftOnAbilityCD(float abilityCD)
    {
        timeRemainingOnCD = abilityCD;
        isAbilityOnCD = true;
    }

    public float GetTimeLeftOnAbilityCD()
    {
        if (isActive)
            return abilityCooldown;
        else 
            return timeRemainingOnCD;
    }

    public float GetAbilityCDOnEntrance()
    {
        return abilityCooldownOnEntrance;
    }
}
