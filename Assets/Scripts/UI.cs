using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject healthGauge;
    [SerializeField] private GameObject ammoGauge;
    [SerializeField] private GameObject speedGauge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthGauge.transform.rotation = Quaternion.Euler(0, 0, 0);
        ammoGauge.transform.rotation = Quaternion.Euler(0, 0, 0);
        speedGauge.transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void updateUI(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        updateHealth(playerScript.GetCar(), playerScript.GetCar());
        updateAmmo(1);
        updateSpeed(1);
    }

    public void updateHealth(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float) currentHealth / (float) maxHealth;
        float angleOfGauge = -90f * (1 - healthPercentage);
        healthGauge.transform.rotation = Quaternion.Euler(0, 0, angleOfGauge);
    }
    public void updateAmmo(int ammo)
    {

    }
    public void updateSpeed(int speed)
    {

    }
}
