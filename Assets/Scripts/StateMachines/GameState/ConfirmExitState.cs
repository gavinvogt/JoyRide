using UnityEngine;

namespace StateMachines.GameState
{
    public class ConfirmExitState : IState
    {
        private GameStateManager _game;

        public ConfirmExitState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("[GameState] Entered ConfirmExitState");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited ConfirmExitState");
        }
    }
}