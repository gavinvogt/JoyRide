using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

public class Missile : MonoBehaviour
{
    private Transform targetPos;
    private Spotlight spotlight;
    private Helicopter helicopter;
    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private GameObject explosion;

    private bool hasCollided = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetPos && Mathf.Abs(targetPos.position.x - this.transform.position.x) + Mathf.Abs(targetPos.position.y - this.transform.position.y) < 0.75f && !hasCollided)
        {
            StartCoroutine(Explode());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car" && !hasCollided)
        {
            StartCoroutine(Explode());
        }
    }

    public void SetReferences(Transform target, Spotlight parentSpotlight, Helicopter heli)
    {
        targetPos = target;
        spotlight = parentSpotlight;
        helicopter = heli;
    }

    IEnumerator Explode()
    {
        hasCollided = true;
        helicopter.RemoveMissileFromList(this.gameObject);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        SoundFXManager.instance.PlaySoundFXClip(hitSoundClip, transform, 1f);
        GameObject hitExplosion = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, -6.1f), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(hitExplosion);
        yield return new WaitForSeconds(0.5f);
        if (spotlight) spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }

    public void disableCollider()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
