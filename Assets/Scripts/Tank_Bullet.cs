using UnityEngine;
using System.Collections;

public class Tank_Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosion;
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

        if (ObjectTags.IsBlockingObstacle(target.gameObject.tag) || target.CompareTag("Enemy"))
        {
            StartCoroutine(CreateExplosion());
        }
    }

    private IEnumerator CreateExplosion()
    {
        hasHit = true;
        GameObject hitExplosion = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, -6.1f), this.transform.rotation, this.transform);
        hitExplosion.GetComponent<RocketGunExplosion>().SetDamage(damage);
        SoundFXManager.instance.PlayRandomSoundFXClip(hitSounds, transform, 0.4f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.15f);
        Destroy(hitExplosion);
        Destroy(gameObject);
    }
}
