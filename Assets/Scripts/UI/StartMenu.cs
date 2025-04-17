using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class StartMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private UIDocument _controlsDocument;
    [SerializeField] private UIDocument _carCollectionDocument;
    private Button _startButton;
    private Button _carsButton;
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
        _carsButton = _document.rootVisualElement.Q<Button>(UIElementIds.CARS_BUTTON);
        _controlsButton = _document.rootVisualElement.Q<Button>(UIElementIds.CONTROLS_BUTTON);
        _statsButton = _document.rootVisualElement.Q<Button>(UIElementIds.STATS_BUTTON);
        _quitButton = _document.rootVisualElement.Q<Button>(UIElementIds.QUIT_BUTTON);
    }

    private void AddEventListeners()
    {
        _startButton.clicked += HandleStart;
        _carsButton.clicked += HandleCarCollection;
        _controlsButton.clicked += HandleControls;
        _statsButton.clicked += HandleStats;
        _quitButton.clicked += HandleQuit;
    }

    private void RemoveEventListeners()
    {
        _startButton.clicked -= HandleStart;
        _carsButton.clicked -= HandleCarCollection;
        _controlsButton.clicked -= HandleControls;
        _statsButton.clicked -= HandleStats;
        _quitButton.clicked -= HandleQuit;
    }

    private void HandleStart()
    {
        SceneManager.LoadScene(sceneName: GameScenes.BuildScene);
    }

    private void HandleCarCollection()
    {
        _carCollectionDocument.rootVisualElement.visible = true;
        CarSelectionMenu.BackButton.RegisterCallbackOnce<ClickEvent>(_ =>
        {
            _carCollectionDocument.rootVisualElement.visible = false;
        });
    }

    private void HandleControls()
    {
        _controlsDocument.rootVisualElement.visible = true;
        ControlsMenu.ConfirmButton.RegisterCallbackOnce<ClickEvent>(_ =>
        {
            _controlsDocument.rootVisualElement.visible = false;
        });
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
