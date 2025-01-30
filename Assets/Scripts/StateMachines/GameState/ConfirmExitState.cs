using UnityEngine;
using UnityEngine.UIElements;

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
            Debug.Log("[GameState] Entered ConfirmExitState");
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
            Debug.Log("[GameState] Exited ConfirmExitState");
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
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.inGameMenuState);
        }

        private void ConfirmExit()
        {
            Application.Quit();
        }
    }
}