using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines.GameState
{
    public class StartGameState : IState
    {
        private GameStateManager _game;

        public StartGameState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("[GameState] Entered StartGameState");
            SceneManager.LoadScene(sceneName: GameScenes.BuildScene);
        }

        public void Execute()
        {
            // Once game is started, transition to in-game state
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.inGameState);
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited StartGameState");
        }
    }
}