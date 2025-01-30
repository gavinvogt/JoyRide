using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameMenu : MonoBehaviour
{
    public static event Action<float> MasterVolumeChanged;
    public static event Action<float> SoundFXVolumeChanged;
    public static event Action<float> MusicVolumeChanged;

    [SerializeField] private UIDocument _document;
    public static Button HelpButton { get; private set; }
    public static Button HomeButton { get; private set; }
    public static Button ContinueButton { get; private set; }
    private Slider _masterSlider;
    private Slider _soundFXSlider;
    private Slider _musicSlider;

    private void Awake()
    {
        FindElements();
        AddEventListeners();
        _document.rootVisualElement.visible = false;
    }

    private void FindElements()
    {
        // buttons
        var root = _document.rootVisualElement;
        HelpButton = root.Q<Button>(UIElementIds.HELP_BUTTON);
        HomeButton = root.Q<Button>(UIElementIds.HOME_BUTTON);
        ContinueButton = root.Q<Button>(UIElementIds.CONTINUE_BUTTON);

        // prepare volume sliders
        _masterSlider = root.Q<Slider>(UIElementIds.MASTER_VOLUME_SLIDER);
        _soundFXSlider = root.Q<Slider>(UIElementIds.SOUND_FX_VOLUME_SLIDER);
        _musicSlider = root.Q<Slider>(UIElementIds.MUSIC_VOLUME_SLIDER);
    }

    private void AddEventListeners()
    {
        // volume slider listeners
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
