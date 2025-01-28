using StateMachines.GameState;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStateManager : MonoBehaviour
{
    [DoNotSerialize] public GameStateMachine gameStateMachine;

    // Serialized fields on the game manager to be used as context
    [SerializeField] private SoundMixerManager _soundMixerManager; // TODO: can likely remove this context since the class has static members
    public SoundMixerManager SoundMixerManager
    {
        get { return _soundMixerManager; }
    }
    [SerializeField] private UIDocument _inGameMenuDocument;
    public UIDocument InGameMenuDocument
    {
        get { return _inGameMenuDocument; }
    }
    [SerializeField] private UIDocument _confirmExitModalDocument;
    public UIDocument ConfirmExitModalDocument
    {
        get { return _confirmExitModalDocument; }
    }

    private void Awake()
    {
        gameStateMachine = new(this);
    }

    private void Start()
    {
        gameStateMachine.Initialize(gameStateMachine.inGameState);
    }

    private void Update()
    {
        gameStateMachine.Execute();
    }
}
