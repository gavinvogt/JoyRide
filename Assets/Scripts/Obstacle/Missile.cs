using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private Transform targetPos;
    private Spotlight spotlight;
    [SerializeField] private AudioClip hitSoundClip;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > targetPos.position.x - .5f && transform.position.x < targetPos.position.x + .5f &&
            transform.position.y > targetPos.position.y - .5f && transform.position.y < targetPos.position.y + .5f)
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
        yield return new WaitForSeconds(1f);
        spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }
}
