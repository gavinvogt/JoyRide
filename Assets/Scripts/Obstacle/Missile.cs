using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private Transform targetPos;
    private Spotlight spotlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((this.transform.position.x > targetPos.position.x - .1f && this.transform.position.x < targetPos.position.x + .1f) &&
            this.transform.position.y > targetPos.position.y - .1f && this.transform.position.y < targetPos.position.y + .1f)
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
        yield return new WaitForSeconds(.2f);
        spotlight.StartCoroutine(spotlight.Move());
        Destroy(gameObject);
    }
}
