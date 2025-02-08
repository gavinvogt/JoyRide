using UnityEngine;
using System.Collections;

public class RocketGunExplosion : MonoBehaviour
{
    private int damage;

    void Awake()
    {
        StartCoroutine(ColliderDisable());
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            if (target.gameObject) target.gameObject.SendMessage("DecreaseHealthNoDamageMarker", damage);
        }
    }

    public void SetDamage(int damageFromGun)
    {
        damage = damageFromGun;
    }

    IEnumerator ColliderDisable()
    {
        yield return new WaitForSeconds(0.15f);
        disableCollider();
    }

    public void disableCollider()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
