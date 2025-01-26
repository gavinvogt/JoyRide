using UnityEngine;

namespace StateMachines.GameState
{
    public class DeathStillFrameState : IState
    {
        private GameStateManager _game;

        public DeathStillFrameState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("[GameState] Entered DeathStillFrameState");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited DeathStillFrameState");
        }
    }
}