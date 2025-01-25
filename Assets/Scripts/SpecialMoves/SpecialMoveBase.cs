using UnityEngine;

public abstract class SpecialMoveBase : MonoBehaviour
{
    /// <summary>
    /// Activates the car's special move
    /// </summary>
    public abstract void ActivateSpecialMove();

    /// <summary>
    /// Ends the car's special move
    /// </summary>
    public abstract void EndSpecialMove();
}
