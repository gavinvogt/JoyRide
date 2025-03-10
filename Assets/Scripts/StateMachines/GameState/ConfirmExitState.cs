using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines.GameState
{
    public class ConfirmExitState : IState
    {
        private GameStateManager _game;
        private bool isMenuActive = false;

        public ConfirmExitState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            if (!isMenuActive)
            {
                SetMenuDisplayOn(true);
                ConfirmExitModal.CancelButton.clicked += CancelExit;
                ConfirmExitModal.ConfirmButton.clicked += ConfirmExit;
            }
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CancelExit();
        }

        public void Exit()
        {
            SetMenuDisplayOn(false);
            ConfirmExitModal.CancelButton.clicked -= CancelExit;
            ConfirmExitModal.ConfirmButton.clicked -= ConfirmExit;
        }

        private void SetMenuDisplayOn(bool toDisplay)
        {
            _game.ConfirmExitModalDocument.rootVisualElement.visible = toDisplay;
            isMenuActive = toDisplay;
        }

        private void CancelExit()
        {
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.PreviousState);
        }

        private void ConfirmExit()
        {
            SceneManager.LoadScene(sceneName: GameScenes.StartMenu);
        }
    }
}