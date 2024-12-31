using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTextElement;
    [SerializeField] private TextMeshProUGUI scoreTextElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeTextElement.text = GameTimer.instance.GetTimeString();
        scoreTextElement.text = "Score: " + GameScore.instance.GetScoreString();
    }
}
