using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    private Texture2D[] cachedImages;

    private static readonly string CAR_COLLECTION_SCROLL_VIEW = "ScrollView";
    private static readonly string CAR_DETAILS_TITLE = "CarDetailsTitle";
    private static readonly string CAR_DETAILS_IMAGE = "CarDetailsImage";
    /** Contains a row of stats for the car, e.g. Speed */
    private static readonly string CAR_STATS_CONTAINER = "CarStatsContainer";
    private static readonly string CAR_ABILITY_DESCRIPTION = "CarAbilityDescription";

    private void Awake()
    {
        _document.rootVisualElement.visible = false;
        cachedImages = CarProperties.Values.Select(props =>
            AssetDatabase.LoadAssetAtPath<Texture2D>(props.SmallImage)
        ).ToArray();

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
        CarProperties[] properties = CarProperties.Values.ToArray();
        for (int i = 0; i < properties.Length; ++i)
        {
            _carCollectionScrollView.Add(CreateCarTile(properties[i], i));
        }
    }

    private VisualElement CreateCarTile(CarProperties props, int index)
    {
        VisualElement carTile = new();
        carTile.AddToClassList("car-tile");

        VisualElement carImage = new();
        carImage.AddToClassList("car-image");
        carImage.style.backgroundImage = cachedImages[index];

        Label carLabel = new(props.Name);
        carLabel.AddToClassList("car-label");

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
        _carDetailsImage.style.backgroundImage = cachedImages[_selectedCarIndex];

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
