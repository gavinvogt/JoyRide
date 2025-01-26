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
            Debug.Log("[GameState] Entered InGameState");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited InGameState");
        }
    }
}