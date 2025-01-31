using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    public static Button ConfirmButton { get; private set; }

    private void Awake()
    {
        FindElements();
        _document.rootVisualElement.visible = false;
    }

    private void FindElements()
    {
        var root = _document.rootVisualElement;
        ConfirmButton = root.Q<Button>(UIElementIds.CONFIRM_BUTTON);
    }
}
