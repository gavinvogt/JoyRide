using System.Collections;
using UnityEditor;
using UnityEngine;
using static Booster;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthGauge;
    [SerializeField] private GameObject ammoGauge;
    [SerializeField] private GameObject speedGauge;
    [SerializeField] private GameObject boost;

    [SerializeField] private GameObject healthPadIndicator;
    [SerializeField] private GameObject ammoPadIndicator;
    [SerializeField] private GameObject speedPadIndicator;
    [SerializeField] private GameObject goonPadIndicator;

    private static bool isBoostIndicatorShowing = false;

    void Awake()
    {
        DisableSpeedBoostUI();
        updateUI(player);
    }

    public void updateUI(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript.GetCar())
        {
            Car car = playerScript.GetCar().GetComponent<Car>();
            updateHealth(car.GetHealthPercentage());
            updateAmmo(car.GetComponent<Car>().GetAmmoPercentage());
        }
        updateSpeed(playerScript.GetSpeedPercentage());
    }

    public void updateHealth(float healthPercentage)
    {
        float angleOfGauge = -90f * (1 - healthPercentage);
        healthGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
    }
    public void updateAmmo(float ammoPercentage)
    {
        float angleOfGauge = 90f * (1 - ammoPercentage);
        ammoGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
    }
    public void updateSpeed(float speedPercentage)
    {
        if (!float.IsNaN(speedPercentage)) {
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

    public IEnumerator ActivateBoostPadIndicator(BoosterType type)
    {
        yield return new WaitUntil(() => !isBoostIndicatorShowing); //Blocking loop to only allow one thing to go at a time, hopefully this kinda queues them up but we will see
        isBoostIndicatorShowing = true;
        BoostIndicator statPadActive = null;
        switch (type)
        {
            case BoosterType.HEALTH:
                statPadActive = healthPadIndicator.GetComponent<BoostIndicator>();
                break;
            case BoosterType.AMMO:
                statPadActive = ammoPadIndicator.GetComponent<BoostIndicator>();
                break;
            case BoosterType.HANDLING:;
                statPadActive = speedPadIndicator.GetComponent<BoostIndicator>();
                break;
            case BoosterType.GOONS:
                statPadActive = goonPadIndicator.GetComponent<BoostIndicator>();
                break;
            default:
                statPadActive = new BoostIndicator();
                break;
        }
        statPadActive.Activate();
        yield return new WaitForSeconds(0.5f);
        statPadActive.Deactivate();
        isBoostIndicatorShowing = false;
    }
}
