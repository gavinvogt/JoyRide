using UnityEngine;
using Utils;

namespace StateMachines.GameState
{
    public class DeathWithinCarState : IState
    {
        private GameStateManager _game;
        private TimeScaleFlipper _timeScaleFlipper = new();
        private float _timeWaited = 0;
        private static float SLOW_TIME = 1.2f;

        public DeathWithinCarState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            _timeScaleFlipper.UpdateTimeScale(0.2f);
        }

        public void Execute()
        {
            // Wait for slow time, then return to 1x speed so car gets collected by ObstacleDestroyer
            _timeWaited += Time.unscaledDeltaTime;
            if (_timeWaited >= SLOW_TIME && _timeScaleFlipper.IsFlipped)
            {
                _timeScaleFlipper.RevertTimeScale();
            }
        }

        public void Exit()
        {
            if (_timeScaleFlipper.IsFlipped) _timeScaleFlipper.RevertTimeScale();
        }
    }
}