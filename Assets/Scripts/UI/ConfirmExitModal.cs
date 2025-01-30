using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ConfirmExitModal : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    public static Button CancelButton { get; private set; }
    public static Button ConfirmButton { get; private set; }

    private void Awake()
    {
        FindElements();
        _document.rootVisualElement.visible = false;
    }

    private void FindElements()
    {
        var root = _document.rootVisualElement;
        CancelButton = root.Q<Button>(UIElementIds.CANCEL_BUTTON);
        ConfirmButton = root.Q<Button>(UIElementIds.CONFIRM_BUTTON);
    }
}
