using UnityEngine;

public class TimeSlowSpecial : SpecialMoveBase
{
    [SerializeField] private float timeSlowFactor;
    [SerializeField] private float timeSlowDuration;
    private float previousTimeScale;
    private float timeSinceActivated;
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
            timeSinceActivated += Time.unscaledDeltaTime;
            if (timeSinceActivated >= timeSlowDuration)
            {
                // End special and restore speed
                EndSpecialMove();
            }
        }
    }

    public override void ActivateSpecialMove()
    {
        if (isActive || isAbilityOnCD) return; // ignore extra special activations
        // TODO: add some special cooldown, with indicator in game UI

        previousTimeScale = Time.timeScale;

        car.SetDrivingSpeed(car.GetDrivingSpeed() * 1f/timeSlowFactor);
        Time.timeScale *= timeSlowFactor;
        timeSinceActivated = 0.0f;

        isActive = true;
    }

    public override void EndSpecialMove()
    {
        Time.timeScale = previousTimeScale;
        car.SetDrivingSpeed(car.GetDrivingSpeed() * timeSlowFactor);
        isActive = false;
        StartAbilityCD();
    }

    public void OnDestroy()
    {
        if (isActive)
        {
            Time.timeScale = previousTimeScale;
            car.SetDrivingSpeed(car.GetDrivingSpeed() * timeSlowFactor);
        }
    }
}
