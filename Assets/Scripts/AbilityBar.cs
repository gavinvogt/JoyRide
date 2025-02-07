using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    private float maxCooldown;

    public void SetInitialAbilityCD(float maxCD, float currentCD)
    {
        maxCooldown = maxCD;
        slider.maxValue = maxCooldown;
        slider.value = CurrentCooldownPercentage(currentCD);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetCurrentAbilityCD(float currentCD)
    {
        slider.value = CurrentCooldownPercentage(currentCD);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private float CurrentCooldownPercentage(float currCD)
    {
        float fillOfBar = maxCooldown - currCD;
        return fillOfBar;
    }
}
