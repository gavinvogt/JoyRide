using System;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace StateMachines.GameState
{
    public class InGameMenuState : IState
    {
        private GameStateManager _game;
        private TimeScaleFlipper _timeScaleFlipper = new();
        private bool isMenuActive = false;

        public InGameMenuState(GameStateManager game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("[GameState] Entered InGameMenuState");
            // Pause game (if not paused)
            if (!_timeScaleFlipper.IsFlipped) _timeScaleFlipper.UpdateTimeScale(0);
            // Display menu (if not yet shown)
            if (!isMenuActive)
            {
                SetMenuDisplayOn(true);
                SetInitialVolumes();
            }

            // Listen to volume change events from the menu
            InGameMenu.MasterVolumeChanged += HandleMasterVolumeChanged;
            InGameMenu.SoundFXVolumeChanged += HandleSoundFXVolumeChanged;
            InGameMenu.MusicVolumeChanged += HandleMusicVolumeChanged;

            // Listen to button click events
            InGameMenu.HomeButton.clicked += HandleHomeButtonClick;
            InGameMenu.ContinueButton.clicked += UnpauseGame;
            InGameMenu.HelpButton.clicked += HandleHelpButtonClick;
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpauseGame();
            }
        }

        public void Exit()
        {
            Debug.Log("[GameState] Exited InGameMenuState");
            InGameMenu.HomeButton.clicked -= HandleHomeButtonClick;
            InGameMenu.ContinueButton.clicked -= UnpauseGame;
            InGameMenu.HelpButton.clicked -= HandleHelpButtonClick;

            InGameMenu.MasterVolumeChanged -= HandleMasterVolumeChanged;
            InGameMenu.SoundFXVolumeChanged -= HandleSoundFXVolumeChanged;
            InGameMenu.MusicVolumeChanged -= HandleMusicVolumeChanged;

            // Save the sound changes to file
            Save.globalSaveData.SetVolumeValues(SoundMixerManager.GetSoundVolumeInSaveFormat());
        }

        private void HandleMasterVolumeChanged(float newValue)
        {
            _game.SoundMixerManager.SetMasterVolume(newValue);
        }

        private void HandleSoundFXVolumeChanged(float newValue)
        {
            _game.SoundMixerManager.SetSoundFXVolume(newValue);
        }

        private void HandleMusicVolumeChanged(float newValue)
        {
            _game.SoundMixerManager.SetMusicVolume(newValue);
        }

        private void SetMenuDisplayOn(bool toDisplay)
        {
            _game.InGameMenuDocument.rootVisualElement.visible = toDisplay;
            isMenuActive = toDisplay;
        }

        private void SetInitialVolumes()
        {
            var volumeKeyValues = new Tuple<string, float>[] {
                new(UIElementIds.MASTER_VOLUME_SLIDER, SoundMixerManager.GetMasterVolumeLevel()),
                new(UIElementIds.SOUND_FX_VOLUME_SLIDER, SoundMixerManager.GetSoundFXVolumeLevel()),
                new(UIElementIds.MUSIC_VOLUME_SLIDER, SoundMixerManager.GetMusicVolumeLevel())
            };
            foreach (var (sliderId, val) in volumeKeyValues)
            {
                _game.InGameMenuDocument.rootVisualElement.Q<Slider>(sliderId).SetValueWithoutNotify(val);
            }
        }

        private void HandleHomeButtonClick()
        {
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.confirmExitState);
        }

        private void UnpauseGame()
        {
            SetMenuDisplayOn(false);
            _timeScaleFlipper.RevertTimeScale();
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.inGameState);
        }

        private void HandleHelpButtonClick()
        {
            _game.gameStateMachine.TransitionTo(_game.gameStateMachine.showControlsState);
        }
    }
}