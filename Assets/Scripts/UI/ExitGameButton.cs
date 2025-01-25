using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitGameButton : MonoBehaviour
{
    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(Application.Quit);
    }
}
