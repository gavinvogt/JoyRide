using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    private Button exitButton;

    void Start()
    {
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
