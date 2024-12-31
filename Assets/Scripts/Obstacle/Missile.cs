using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private Transform targetPos;
    private Spotlight spotlight;
    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private GameObject explosion;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(this.transform.position, targetPos.position) < 1f)
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
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        SoundFXManager.instance.PlaySoundFXClip(hitSoundClip, transform, 1f);
        GameObject hitExplosion = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, -100.5f), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(hitExplosion);
        yield return new WaitForSeconds(0.5f);
        spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }
}
