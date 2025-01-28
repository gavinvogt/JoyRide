using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameMenu : MonoBehaviour
{
    // public static event Action<string> ButtonClicked;
    public static event Action<float> MasterVolumeChanged;
    public static event Action<float> SoundFXVolumeChanged;
    public static event Action<float> MusicVolumeChanged;

    [SerializeField] private UIDocument _document;
    private Button _helpButton;
    private Button _homeButton;
    private Button _continueButton;
    private Slider _masterSlider;
    private Slider _soundFXSlider;
    private Slider _musicSlider;

    private void Awake()
    {
        var root = _document.rootVisualElement;
        _helpButton = root.Q<Button>(UIElementIds.HELP_BUTTON);
        _homeButton = root.Q<Button>(UIElementIds.HOME_BUTTON);
        _continueButton = root.Q<Button>(UIElementIds.CONTINUE_BUTTON);

        // prepare volume sliders
        _masterSlider = root.Q<Slider>(UIElementIds.MASTER_VOLUME_SLIDER);
        _soundFXSlider = root.Q<Slider>(UIElementIds.SOUND_FX_VOLUME_SLIDER);
        _musicSlider = root.Q<Slider>(UIElementIds.MUSIC_VOLUME_SLIDER);

        _masterSlider.RegisterValueChangedCallback(HandleMasterVolumeChanged);
        _soundFXSlider.RegisterValueChangedCallback(HandleSoundFXVolumeChanged);
        _musicSlider.RegisterValueChangedCallback(HandleMusicVolumeChanged);
    }

    private void OnDestroy()
    {
        _masterSlider.UnregisterValueChangedCallback(HandleMasterVolumeChanged);
        _masterSlider.UnregisterValueChangedCallback(HandleSoundFXVolumeChanged);
        _masterSlider.UnregisterValueChangedCallback(HandleMusicVolumeChanged);
    }

    private void HandleMasterVolumeChanged(ChangeEvent<float> evt)
    {
        MasterVolumeChanged?.Invoke(evt.newValue);
    }
    private void HandleSoundFXVolumeChanged(ChangeEvent<float> evt)
    {
        SoundFXVolumeChanged?.Invoke(evt.newValue);
    }
    private void HandleMusicVolumeChanged(ChangeEvent<float> evt)
    {
        MusicVolumeChanged?.Invoke(evt.newValue);
    }
}
