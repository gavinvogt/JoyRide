using UnityEngine;

public class TimeSlowSpecial : SpecialMoveBase
{
    [SerializeField] private float timeSlowFactor;
    [SerializeField] private float timeSlowDuration;
    private float previousTimeScale;
    private float timeSinceActivated;
    private bool isActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Started time slow special script");
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceActivated += Time.unscaledDeltaTime;
        if (timeSinceActivated >= timeSlowDuration)
        {
            // End special and restore speed
            Time.timeScale = previousTimeScale;
            Debug.Log("Ended special, time since activated was " + timeSinceActivated);
            this.enabled = false;
        }
    }

    public override void ActivateSpecialMove()
    {
        if (isActive) return; // ignore extra special activations
        // TODO: add some special cooldown, with indicator in game UI

        Debug.Log("Activating time slow");
        previousTimeScale = Time.timeScale;

        Time.timeScale *= timeSlowFactor;
        timeSinceActivated = 0.0f;
    }
}
