using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthGauge;
    [SerializeField] private GameObject ammoGauge;
    [SerializeField] private GameObject speedGauge;
    [SerializeField] private GameObject boost;

    void Awake()
    {
        DisableBoostUI();
        updateUI(player);
    }

    public void updateUI(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript.GetCar())
        {
            Car car = playerScript.GetCar().GetComponent<Car>();
            updateHealth(car.getHealthPercentage());
            updateAmmo(car.GetComponent<Car>().getAmmoPercentage());
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

    public void EnableBoostUI()
    {
        boost.SetActive(true);
    }

    public void DisableBoostUI()
    {
        boost.SetActive(false);
    }
}
