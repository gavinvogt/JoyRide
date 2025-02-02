using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class StartMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    private Button _startButton;
    private Button _controlsButton;
    private Button _statsButton;
    private Button _quitButton;

    private void Awake()
    {
        FindElements();
        AddEventListeners();
    }

    private void OnDestroy()
    {
        RemoveEventListeners();
    }

    private void FindElements()
    {
        _startButton = _document.rootVisualElement.Q<Button>(UIElementIds.START_BUTTON);
        _controlsButton = _document.rootVisualElement.Q<Button>(UIElementIds.CONTROLS_BUTTON);
        _statsButton = _document.rootVisualElement.Q<Button>(UIElementIds.STATS_BUTTON);
        _quitButton = _document.rootVisualElement.Q<Button>(UIElementIds.QUIT_BUTTON);
    }

    private void AddEventListeners()
    {
        _startButton.clicked += HandleStart;
        _controlsButton.clicked += HandleControls;
        _statsButton.clicked += HandleStats;
        _quitButton.clicked += HandleQuit;
    }

    private void RemoveEventListeners()
    {
        _startButton.clicked -= HandleStart;
        _controlsButton.clicked -= HandleControls;
        _statsButton.clicked -= HandleStats;
        _quitButton.clicked -= HandleQuit;
    }

    private void HandleStart()
    {
        SceneManager.LoadScene(sceneName: GameScenes.BuildScene);
    }

    private void HandleControls()
    {
        Debug.Log("TODO: display controls menu");
    }

    private void HandleStats()
    {
        Debug.Log("TODO: implement stats tracker");
    }

    private void HandleQuit()
    {
        Application.Quit();
    }
}
