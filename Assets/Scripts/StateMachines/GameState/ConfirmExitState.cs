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
                _game.ConfirmExitModalDocument.rootVisualElement.Q<Button>(UIElementIds.CANCEL_BUTTON).RegisterCallbackOnce<ClickEvent>(CancelExit);
                _game.ConfirmExitModalDocument.rootVisualElement.Q<Button>(UIElementIds.CONFIRM_BUTTON).RegisterCallbackOnce<ClickEvent>(ConfirmExit);
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
        }

        private void SetMenuDisplayOn(bool toDisplay)
        {
            _game.ConfirmExitModalDocument.gameObject.SetActive(toDisplay);
            isMenuActive = toDisplay;
        }

        private void CancelExit()
        {
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.inGameMenuState);
        }

        private void CancelExit(ClickEvent _)
        {
            CancelExit();
        }

        private void ConfirmExit(ClickEvent _)
        {
            Application.Quit();
        }
    }
}