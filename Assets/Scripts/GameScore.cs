using System;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textElement;
    [SerializeField] private int pointsPerPoliceCar;
    [SerializeField] private int pointsPerHelicopter;
    [SerializeField] private int pointsPerLaser;
    [SerializeField] private int timeMultiplier;
    private int policeCarsDestroyed = 0;
    private int helicoptersDestroyed = 0;
    private int lasersDestroyed = 0;
    public static GameScore instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        instance.policeCarsDestroyed = 0;
        instance.helicoptersDestroyed = 0;
        instance.lasersDestroyed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = GetScoreString();
    }

    public void IncrementPoliceCarsDestroyed()
    {
        ++instance.policeCarsDestroyed;
    }

    public void IncrementHelicoptersDestroyed()
    {
        ++instance.helicoptersDestroyed;
    }

    public void IncrementLasersDestroyed()
    {
        ++instance.lasersDestroyed;
    }

    public int GetScore()
    {
        Debug.Log("Game time: " + GameTimer.instance.TimerValue);
        Debug.Log("Value is " + (int)Math.Floor(timeMultiplier * GameTimer.instance.TimerValue));
        return (int)Math.Floor(timeMultiplier * GameTimer.instance.TimerValue)
            + pointsPerPoliceCar * instance.policeCarsDestroyed
            + pointsPerHelicopter * instance.helicoptersDestroyed
            + pointsPerLaser * instance.lasersDestroyed;
    }

    public string GetScoreString()
    {
        return GetScore().ToString();
    }
}
