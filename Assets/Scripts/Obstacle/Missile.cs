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
        yield return new WaitForSeconds(.5f);
        spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }
}
