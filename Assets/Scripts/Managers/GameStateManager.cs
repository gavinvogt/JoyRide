using StateMachines.GameState;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }
    [DoNotSerialize] public GameStateMachine gameStateMachine;
    public SoundMixerManager soundMixerManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameStateMachine = new(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
