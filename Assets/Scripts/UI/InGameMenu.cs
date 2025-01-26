using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameMenu : MonoBehaviour
{
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
        _helpButton = root.Q<Button>("HelpButton");
        _homeButton = root.Q<Button>("HomeButton");
        _continueButton = root.Q<Button>("ContinueButton");

        _helpButton.RegisterCallback<ClickEvent>(HandleHelpButtonClick);
        _homeButton.RegisterCallback<ClickEvent>(HandleHomeButtonClick);
        _continueButton.RegisterCallback<ClickEvent>(HandleContinueButtonClick);

        // prepare volume sliders
        _masterSlider = root.Q<Slider>("MasterVolumeSlider");
        _soundFXSlider = root.Q<Slider>("SoundFXVolumeSlider");
        _musicSlider = root.Q<Slider>("MusicVolumeSlider");

        _masterSlider.RegisterValueChangedCallback(v => MasterVolumeChanged?.Invoke(v.newValue));
        _soundFXSlider.RegisterValueChangedCallback(v => SoundFXVolumeChanged?.Invoke(v.newValue));
        _musicSlider.RegisterValueChangedCallback(v => MusicVolumeChanged?.Invoke(v.newValue));
    }

    private void OnDisable()
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
