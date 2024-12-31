using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTextElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeTextElement.text = GameTimer.instance.GetTime();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
