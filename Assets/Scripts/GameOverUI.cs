using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTextElement;
    [SerializeField] private TextMeshProUGUI scoreTextElement;
    [SerializeField] private TextMeshProUGUI highScoreTextElement;
    [SerializeField] private TextMeshProUGUI longestTimeAliveTextElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeTextElement.text = GameTimer.instance.GetTimeString();
        scoreTextElement.text = "Score: " + GameScore.instance.GetScoreString();

        highScoreTextElement.text = "High Score: " + Save.globalSaveData.AttemptSetHighScore(GameScore.instance.GetScore());
        longestTimeAliveTextElement.text = "Longest Time Alive: " + Save.globalSaveData.AttemptSetLongestTimeAlive(GameTimer.instance.GetTimeString());
    }
}
