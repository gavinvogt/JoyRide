using UnityEngine;
using System.Collections;

public class Missile_Explosion : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(ColliderDisable());
    }

    IEnumerator ColliderDisable()
    {
        yield return new WaitForSeconds(0.2f);
        disableCollider();
    }

    public void disableCollider()
    {
        Debug.Log("Calling disable collider");
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log(this.gameObject.GetComponent<CircleCollider2D>().enabled);
    }
}
