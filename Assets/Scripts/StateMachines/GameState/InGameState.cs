using UnityEngine;

namespace StateMachines.GameState
{
    public class InGameState : IState
    {
        private GameStateManager _game;

        public InGameState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Pause game
                _game.gameStateMachine.TransitionTo(_game.gameStateMachine.inGameMenuState);
            }
        }

        public void Exit()
        {
        }
    }
}