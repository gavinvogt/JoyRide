using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip hitSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            PlaySound();
        }
        else if (target.CompareTag("Enemy"))
        {
            target.gameObject.GetComponent<PoliceCar>().DecreaseHealth(damage);
            PlaySound();
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(hitSound, transform, 1f);
    }
}
