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
        timerValue += Time.deltaTime;
        textElement.text = GetTime();
    }

    public string GetTime()
    {
        return TimeSpan.FromSeconds(timerValue).ToString(@"mm\:ss\.f");
    }
}
