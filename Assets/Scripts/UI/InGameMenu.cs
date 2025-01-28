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

    [SerializeField]
    private UIDocument _document;
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

        _helpButton.RegisterCallback<ClickEvent>(HandleHelpButtonClick);
        _homeButton.RegisterCallback<ClickEvent>(HandleHomeButtonClick);
        _continueButton.RegisterCallback<ClickEvent>(HandleContinueButtonClick);

        // prepare volume sliders
        _masterSlider = root.Q<Slider>(UIElementIds.MASTER_VOLUME_SLIDER);
        _soundFXSlider = root.Q<Slider>(UIElementIds.SOUND_FX_VOLUME_SLIDER);
        _musicSlider = root.Q<Slider>(UIElementIds.MUSIC_VOLUME_SLIDER);

        _masterSlider.RegisterValueChangedCallback(v => MasterVolumeChanged?.Invoke(v.newValue));
        _soundFXSlider.RegisterValueChangedCallback(v => SoundFXVolumeChanged?.Invoke(v.newValue));
        _musicSlider.RegisterValueChangedCallback(v => MusicVolumeChanged?.Invoke(v.newValue));
    }

    private void OnDestroy()
    {
        _helpButton.UnregisterCallback<ClickEvent>(HandleHelpButtonClick);
        _homeButton.UnregisterCallback<ClickEvent>(HandleHomeButtonClick);
        _continueButton.UnregisterCallback<ClickEvent>(HandleContinueButtonClick);
    }

    private void HandleHelpButtonClick(ClickEvent evt)
    {
        // TODO: create a Help menu that shows the controls
        Debug.Log("Clicked help button");
    }

    private void HandleHomeButtonClick(ClickEvent evt)
    {
        // TODO: confirm and then return to home screen
        Debug.Log("Clicked home button");
    }

    private void HandleContinueButtonClick(ClickEvent evt)
    {
        // TODO: return to game
        Debug.Log("Clicked continue button");
    }
}
