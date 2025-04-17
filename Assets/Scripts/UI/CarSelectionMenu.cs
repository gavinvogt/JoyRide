using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class CarSelectionMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    private ScrollView _carCollectionScrollView;
    private Label _carDetailsTitle;
    private VisualElement _carDetailsImage;
    private List<VisualElement> _carStatsContainers;
    private Label _carAbilityDescription;

    private static readonly string CAR_COLLECTION_SCROLL_VIEW = "ScrollView";
    private static readonly string CAR_DETAILS_TITLE = "CarDetailsTitle";
    private static readonly string CAR_DETAILS_IMAGE = "CarDetailsImage";
    /** Contains a row of stats for the car, e.g. Speed */
    private static readonly string CAR_STATS_CONTAINER = "CarStatsContainer";
    private static readonly string CAR_ABILITY_DESCRIPTION = "CarAbilityDescription";

    void Awake()
    {
        FindElements();
    }

    private void FindElements()
    {
        var root = _document.rootVisualElement;
        _carCollectionScrollView = root.Q<ScrollView>(CAR_COLLECTION_SCROLL_VIEW);
        _carDetailsTitle = root.Q<Label>(CAR_DETAILS_TITLE);
        _carDetailsImage = root.Q<VisualElement>(CAR_DETAILS_IMAGE);
        _carStatsContainers = root.Query<VisualElement>(CAR_STATS_CONTAINER).ToList();
        _carAbilityDescription = root.Q<Label>(CAR_ABILITY_DESCRIPTION);
    }
}
