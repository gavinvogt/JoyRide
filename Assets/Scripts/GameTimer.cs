using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textElement;
    private float timerValue = 0.0f;
    public static GameTimer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        instance.timerValue = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        instance.timerValue += Time.deltaTime;
        textElement.text = GetTimeString();
    }

    public float TimerValue
    {
        get { return instance.timerValue; }
    }

    public string GetTimeString()
    {
        return TimeSpan.FromSeconds(timerValue).ToString(@"mm\:ss\.f");
    }
}
