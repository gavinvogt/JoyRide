using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines.GameState
{
    public class EndScreenState : IState
    {
        private GameStateManager _game;

        public EndScreenState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            SceneManager.LoadScene(sceneName: GameScenes.EndScreen);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}