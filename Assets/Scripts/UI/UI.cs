using System.Collections;
using UnityEngine;
using static Booster;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthGauge;
    [SerializeField] private GameObject ammoGauge;
    [SerializeField] private GameObject speedGauge;
    [SerializeField] private GameObject boost;

    [SerializeField] private BoostIndicator healthPadIndicator;
    [SerializeField] private BoostIndicator ammoPadIndicator;
    [SerializeField] private BoostIndicator speedPadIndicator;
    [SerializeField] private BoostIndicator goonPadIndicator;

    void Awake()
    {
        DisableSpeedBoostUI();
        UpdateUI(player);
    }

    public void UpdateUI(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript.GetCar())
        {
            Car car = playerScript.GetCar().GetComponent<Car>();
            UpdateHealth(car.GetHealthPercentage());
            UpdateAmmo(car.GetComponent<Car>().GetAmmoPercentage());
        }
        UpdateSpeed(playerScript.GetSpeedPercentage());
    }

    public void UpdateHealth(float healthPercentage)
    {
        float angleOfGauge = -90f * (1 - healthPercentage);
        healthGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
    }
    public void UpdateAmmo(float ammoPercentage)
    {
        float angleOfGauge = 90f * (1 - ammoPercentage);
        ammoGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
    }
    public void UpdateSpeed(float speedPercentage)
    {
        if (!float.IsNaN(speedPercentage))
        {
            float angleOfGauge = 90f + (180 * speedPercentage);
            speedGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
        }
    }

    public void EnableSpeedBoostUI()
    {
        boost.SetActive(true);
    }

    public void DisableSpeedBoostUI()
    {
        boost.SetActive(false);
    }

    public IEnumerator ActivateBoostPadIndicator(BoosterType type, Vector2 carPos)
    {
        BoostIndicator statPadActive;
        switch (type)
        {
            case BoosterType.HEALTH:
                statPadActive = healthPadIndicator;
                break;
            case BoosterType.AMMO:
                statPadActive = ammoPadIndicator;
                break;
            case BoosterType.HANDLING:
                ;
                statPadActive = speedPadIndicator;
                break;
            case BoosterType.GOONS:
                statPadActive = goonPadIndicator;
                break;
            default:
                //Should never hit this case
                statPadActive = null;
                break;
        }
        statPadActive.Activate(carPos);
        yield return new WaitForSeconds(0.5f);
        statPadActive.Deactivate();
    }
}
