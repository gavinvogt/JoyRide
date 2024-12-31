using UnityEngine;
using System.Collections;
using System.Reflection;

public class Missile : MonoBehaviour
{
    private Transform targetPos;
    private Spotlight spotlight;
    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private GameObject explosion;

    private bool hasCollided = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(targetPos.position.x - this.transform.position.x) + Mathf.Abs(targetPos.position.y - this.transform.position.y) < 0.75f && !hasCollided)
        {
            StartCoroutine(Explode());
        }
    }

    public void SetReferences(Transform target, Spotlight parentSpotlight)
    {
        targetPos = target;
        spotlight = parentSpotlight;
    }

    IEnumerator Explode()
    {
        hasCollided = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        SoundFXManager.instance.PlaySoundFXClip(hitSoundClip, transform, 1f);
        GameObject hitExplosion = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, -1.5f), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(hitExplosion);
        yield return new WaitForSeconds(0.5f);
        spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }
}
