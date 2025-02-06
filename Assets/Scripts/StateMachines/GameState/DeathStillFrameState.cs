using UnityEngine;
using Utils;

namespace StateMachines.GameState
{
    public class DeathStillFrameState : IState
    {
        private GameStateManager _game;
        private TimeScaleFlipper _timeScaleFlipper = new();
        private float _timeWaited = 0;
        private static float STILL_TIME = 1.2f;

        public DeathStillFrameState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            _timeScaleFlipper.UpdateTimeScale(0);
        }

        public void Execute()
        {
            _timeWaited += Time.unscaledDeltaTime;
            if (_timeWaited >= STILL_TIME)
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