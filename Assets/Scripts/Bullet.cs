using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip[] hitSounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.up * speed;
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
            target.gameObject.SendMessage("DecreaseHealth", damage);
            PlaySound();
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(hitSounds, transform, 0.5f);
    }
}
