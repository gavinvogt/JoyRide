using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip dieAudioClip;

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
                if (target.gameObject.GetComponent<Car>().IsCarDead())
                {
                    target.GetComponent<Car>().PlayerLoseControl();
                }
                Destroy(gameObject);
            }
        }
        else if (ObjectTags.ShouldKillPlayer(target.gameObject.tag))
        {
            SoundFXManager.instance.PlaySoundFXClip(dieAudioClip, transform, 1f);
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName: GameScenes.EndScreen);
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
