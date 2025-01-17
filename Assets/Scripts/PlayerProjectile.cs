using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    public Player player;
    private GameObject parentCar;
    private bool hasSwitched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Car") && target.gameObject != parentCar)
        {
            if (!hasSwitched)
            {
                player.UpdateCar(target.gameObject);
                hasSwitched = true;
                if (target.gameObject.GetComponent<Car>().GetHealthPercentage() <= 0f)
                {
                    target.GetComponent<Car>().PlayerLoseControl();
                }
                Destroy(gameObject);
            }
        }
        else if (ObjectTags.ShouldKillPlayer(target.gameObject.tag))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName: "EndScreen");
        }
    }

    void SetPlayer(Player player)
    {
        this.player = player;
    }

    void SetParent(GameObject parentCar)
    {
        this.parentCar = parentCar;
    }
}
