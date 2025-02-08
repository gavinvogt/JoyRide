using UnityEngine;

namespace StateMachines.GameState
{
    public class ShowControlsState : IState
    {
        private GameStateManager _game;
        private bool isMenuActive = false;

        public ShowControlsState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            if (!isMenuActive)
            {
                SetMenuDisplayOn(true);
                ControlsMenu.ConfirmButton.clicked += CloseMenu;
            }
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseMenu();
        }

        public void Exit()
        {
            SetMenuDisplayOn(false);
            ControlsMenu.ConfirmButton.clicked -= CloseMenu;
        }

        private void SetMenuDisplayOn(bool toDisplay)
        {
            _game.ControlsDocument.rootVisualElement.visible = toDisplay;
            isMenuActive = toDisplay;
        }

        private void CloseMenu()
        {
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.PreviousState);
        }
    }
}