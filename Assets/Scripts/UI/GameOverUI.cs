using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameOver : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    private Button _restartButton;
    private Button _quitButton;

    private static string BEST_TIME_PREFIX = "Best time: ";
    private static string BEST_SCORE_PREFIX = "Highest score: ";
    private static string RUN_TIME_PREFIX = "Time: ";
    private static string RUN_SCORE_PREFIX = "Score: ";

    private void Awake()
    {
        // Populate best scores
        var root = _document.rootVisualElement;
        root.Q<Label>(UIElementIds.BEST_TIME_LABEL).text = BEST_TIME_PREFIX + Save.globalSaveData.AttemptSetLongestTimeAlive(GameTimer.instance.GetTimeString());
        root.Q<Label>(UIElementIds.BEST_SCORE_LABEL).text = BEST_SCORE_PREFIX + Save.globalSaveData.AttemptSetHighScore(GameScore.instance.GetScore());
        // Populate current run scores
        root.Q<Label>(UIElementIds.RUN_TIME_LABEL).text = RUN_TIME_PREFIX + GameTimer.instance.GetTimeString(); ;
        root.Q<Label>(UIElementIds.RUN_SCORE_LABEL).text = RUN_SCORE_PREFIX + GameScore.instance.GetScoreString();

        // Set up button listeners
        _restartButton = root.Q<Button>(UIElementIds.START_BUTTON);
        _quitButton = root.Q<Button>(UIElementIds.QUIT_BUTTON);
        AddEventListeners();
    }

    private void AddEventListeners()
    {
        _restartButton.clicked += OnRestart;
        _quitButton.clicked += OnQuit;
    }

    private void OnDestroy()
    {
        _restartButton.clicked -= OnRestart;
        _quitButton.clicked -= OnQuit;
    }

    private void OnRestart()
    {
        SceneManager.LoadScene(sceneName: GameScenes.BuildScene);
    }

    private void OnQuit()
    {
        SceneManager.LoadScene(sceneName: GameScenes.StartMenu);
    }
}
