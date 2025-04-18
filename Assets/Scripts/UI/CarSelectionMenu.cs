using System.Collections.Generic;
using System.Linq;
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
    public static Button BackButton { get; private set; }
    private int _selectedCarIndex = 0;

    private static readonly string CAR_COLLECTION_SCROLL_VIEW = "ScrollView";
    private static readonly string CAR_DETAILS_TITLE = "CarDetailsTitle";
    private static readonly string CAR_DETAILS_IMAGE = "CarDetailsImage";
    /** Contains a row of stats for the car, e.g. Speed */
    private static readonly string CAR_STATS_CONTAINER = "CarStatsContainer";
    private static readonly string CAR_ABILITY_DESCRIPTION = "CarAbilityDescription";

    private void Awake()
    {
        _document.rootVisualElement.visible = false;
        FindElements();
        CreateCarTiles();
        CreateCarTileListeners();
        UpdateDetailsPanel();
    }

    private void FindElements()
    {
        var root = _document.rootVisualElement;
        _carCollectionScrollView = root.Q<ScrollView>(CAR_COLLECTION_SCROLL_VIEW);
        _carDetailsTitle = root.Q<Label>(CAR_DETAILS_TITLE);
        _carDetailsImage = root.Q<VisualElement>(CAR_DETAILS_IMAGE);
        _carStatsContainers = root.Query<VisualElement>(CAR_STATS_CONTAINER).ToList();
        _carAbilityDescription = root.Q<Label>(CAR_ABILITY_DESCRIPTION);
        BackButton = root.Q<Button>(UIElementIds.BACK_BUTTON);
    }

    private void CreateCarTiles()
    {
        // Create a tile for each car in the game
        _carCollectionScrollView.Clear();
        foreach (var carProperty in CarProperties.Values)
        {
            _carCollectionScrollView.Add(CreateCarTile(carProperty));
        }
    }

    private VisualElement CreateCarTile(CarProperties props)
    {
        VisualElement carTile = new();
        carTile.AddToClassList("car-tile");

        VisualElement carImage = new();
        carImage.AddToClassList("car-image");
        carImage.AddToClassList(ImagePathToClass(props.SmallImage));
        // Example:
        // background-image: url("project://database/Assets/UI/IconBig.png?fileID=2800000&guid=5872c11bd91cf69479e98bca5a7333f8&type=3#IconBig");

        Label carLabel = new(props.Name);
        carImage.AddToClassList("car-label");

        carTile.Add(carImage);
        carTile.Add(carLabel);
        return carTile;
    }

    private void CreateCarTileListeners()
    {
        VisualElement[] carTiles = _carCollectionScrollView.Children().ToArray();
        for (int i = 0; i < carTiles.Length; ++i)
        {
            int index = i;
            carTiles[i].AddManipulator(new Clickable(evt =>
            {
                SetSelectedCar(index);
            }));
        }
        ;
    }

    private void SetSelectedCar(int index)
    {
        _selectedCarIndex = index;
        UpdateDetailsPanel();
    }

    private void UpdateDetailsPanel()
    {
        CarProperties selectedCar = CarProperties.Values.ElementAt(_selectedCarIndex);
        _carDetailsTitle.text = selectedCar.Name;
        _carDetailsImage.AddToClassList(ImagePathToClass(selectedCar.SmallImage));
        // TODO: remove the previous bg-image class

        // Update the stats shown
        UpdateStatValue(_carStatsContainers[0], selectedCar.BaseStats.Speed.ToString());
        UpdateStatValue(_carStatsContainers[1], selectedCar.BaseStats.Health.ToString());
        UpdateStatValue(_carStatsContainers[2], GetDamageString(selectedCar.BaseStats.Damage, selectedCar.BaseStats.BulletsPerShot));
        UpdateStatValue(_carStatsContainers[3], selectedCar.BaseStats.Ammo.ToString());
    }

    private void UpdateStatValue(VisualElement statsContainer, string value)
    {
        statsContainer.Q<Label>("CarStatValue").text = value;
    }

    private static string ImagePathToClass(string imagePath)
        => $"background-image: url(\"{imagePath}\")";

    private static string GetDamageString(float damage, int bulletsPerShot)
    {
        if (bulletsPerShot == 1)
        {
            return damage.ToString();
        }
        else
        {
            return $"{damage} x {bulletsPerShot}";
        }
    }
}
