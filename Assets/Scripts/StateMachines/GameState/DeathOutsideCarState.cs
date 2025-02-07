using UnityEngine;
using Utils;

namespace StateMachines.GameState
{
    public class DeathOutsideCarState : IState
    {
        private GameStateManager _game;
        private TimeScaleFlipper _timeScaleFlipper = new();
        private float _timeWaited = 0;
        private static float SLOW_TIME = 1.2f;

        public DeathOutsideCarState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            _timeScaleFlipper.UpdateTimeScale(0.2f);
        }

        public void Execute()
        {
            // Keep game slowed for wait time, then transition to end screen
            _timeWaited += Time.unscaledDeltaTime;
            if (_timeWaited >= SLOW_TIME)
            {
                _game.gameStateMachine.TransitionTo(_game.gameStateMachine.endScreenState);
            }
        }

        public void Exit()
        {
            _timeScaleFlipper.RevertTimeScale();
        }
    }
}