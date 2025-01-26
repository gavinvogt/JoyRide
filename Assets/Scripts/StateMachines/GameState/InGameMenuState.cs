using UnityEngine;

namespace StateMachines.GameState
{
    public class InGameMenuState : IState
    {
        private GameStateManager _game;

        public InGameMenuState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("[GameState] Entered InGameMenuState");
            InGameMenu.MasterVolumeChanged += HandleMasterVolumeChanged;
            InGameMenu.SoundFXVolumeChanged += HandleSoundFXVolumeChanged;
            InGameMenu.MusicVolumeChanged += HandleMusicVolumeChanged;
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited InGameMenuState");
            InGameMenu.MasterVolumeChanged -= HandleMasterVolumeChanged;
            InGameMenu.SoundFXVolumeChanged -= HandleSoundFXVolumeChanged;
            InGameMenu.MusicVolumeChanged -= HandleMusicVolumeChanged;
        }

        private void HandleMasterVolumeChanged(float newValue)
        {
            Debug.Log("New master volume " + newValue);
            _game.soundMixerManager.SetMasterVolume(newValue);
        }

        private void HandleSoundFXVolumeChanged(float newValue)
        {
            Debug.Log("New SoundFX volume " + newValue);
            _game.soundMixerManager.SetSoundFXVolume(newValue);
        }

        private void HandleMusicVolumeChanged(float newValue)
        {
            Debug.Log("New Music volume " + newValue);
            _game.soundMixerManager.SetMusicVolume(newValue);
        }
    }
}