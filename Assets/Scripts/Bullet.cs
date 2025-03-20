using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    static private float damage;
    [SerializeField] private AudioClip[] hitSounds;
    private bool hasHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (hasHit) return; // ignore collision if a hit has already triggered

        if (target.CompareTag("BoundaryWall"))
        {
            HandleCollisionNoSound();
        }
        else if (ObjectTags.IsBlockingObstacle(target.gameObject.tag))
        {
            HandleCollision();
        }
        else if (target.CompareTag("Enemy"))
        {
            if (target.gameObject) target.gameObject.SendMessage("DecreaseHealth", damage);
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        hasHit = true;
        SoundFXManager.instance.PlayRandomSoundFXClip(hitSounds, transform, 0.6f);
        Destroy(gameObject);
    }

    private void HandleCollisionNoSound()
    {
        hasHit = true;
        Destroy(gameObject);
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
