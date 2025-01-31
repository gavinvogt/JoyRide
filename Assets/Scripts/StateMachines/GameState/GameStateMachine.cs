using System;

namespace StateMachines.GameState
{
    public class GameStateMachine
    {
        public IState CurrentState { get; private set; }

        // state objects
        public InGameState inGameState;
        public DeathStillFrameState deathStillFrameState;
        public EndScreenState endScreenState;
        // menu-related states
        public InGameMenuState inGameMenuState;
        public ShowControlsState showControlsState;
        public ConfirmExitState confirmExitState;

        // event to notify other objects when the state changes
        public event Action<IState> stateChanged;

        public GameStateMachine(GameStateManager game)
        {
            // game-related states
            inGameState = new(game);
            deathStillFrameState = new(game);
            endScreenState = new(game);
            // menu-related states
            showControlsState = new(game);
            inGameMenuState = new(game);
            confirmExitState = new(game);
        }

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            // notify other objects that state changed
            stateChanged?.Invoke(state);
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            // notify other objects that state changed
            stateChanged?.Invoke(nextState);
        }

        public void Execute()
        {
            CurrentState?.Execute();
        }
    }
}