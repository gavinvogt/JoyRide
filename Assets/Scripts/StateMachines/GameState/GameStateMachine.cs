using System;

namespace StateMachines.GameState
{
    public class GameStateMachine
    {
        public IState CurrentState { get; private set; }
        public IState PreviousState { get; private set; }

        // state objects
        public InGameState inGameState;
        public DeathOutsideCarState deathOutsideCarState;
        public DeathWithinCarState deathWithinCarState;
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
            deathOutsideCarState = new(game);
            deathWithinCarState = new(game);
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
            PreviousState = CurrentState;
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