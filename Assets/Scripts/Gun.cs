using UnityEngine;

public class Gun : MonoBehaviour
{
    public Player player;
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Adjust angle of gun by mouse
            var targetAngle = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rot = Mathf.Atan2(targetAngle.y, targetAngle.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rot - 90, Vector3.forward);
        }
    }

    void SetPlayer(Player player)
    {
        this.player = player;
    }

    void Reset()
    {
        player = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
