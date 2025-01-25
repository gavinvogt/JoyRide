using UnityEngine;

public class TimeSlowSpecial : SpecialMoveBase
{
    [SerializeField] private float timeSlowFactor;
    [SerializeField] private float timeSlowDuration;
    private float previousTimeScale;
    private float timeSinceActivated;
    private bool isActive = false;
    Car car;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = this.gameObject.GetComponent<Car>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceActivated += Time.unscaledDeltaTime;
        if (timeSinceActivated >= timeSlowDuration)
        {
            // End special and restore speed
            EndSpecialMove();
        }
    }

    public override void ActivateSpecialMove()
    {
        if (isActive) return; // ignore extra special activations
        // TODO: add some special cooldown, with indicator in game UI

        isActive = true;
        previousTimeScale = Time.timeScale;

        car.SetDrivingSpeed(car.GetDrivingSpeed() * 1f/timeSlowFactor);
        Time.timeScale *= timeSlowFactor;
        timeSinceActivated = 0.0f;
    }

    public override void EndSpecialMove()
    {
        Time.timeScale = previousTimeScale;
        car.SetDrivingSpeed(car.GetDrivingSpeed() * timeSlowFactor);
        isActive = false;
        this.enabled = false;
    }

    public void OnDestroy()
    {
        if (isActive)
            EndSpecialMove();
    }
}
