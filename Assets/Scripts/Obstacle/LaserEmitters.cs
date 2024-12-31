using UnityEngine;

public class LaserEmitters : MonoBehaviour
{
    [SerializeField] private Laser laser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth(int damage)
    {
        if(laser) laser.DecreaseHealth(damage);
    }
}
