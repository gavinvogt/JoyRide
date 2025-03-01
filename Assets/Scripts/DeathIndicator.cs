using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeathIndicator : MonoBehaviour
{
    void Start()
    {
        transform.localScale *= 0.2f;
        StartCoroutine(IndicatorSizeIncrease());
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1f * GameObject.Find("Highway").GetComponentInChildren<ObstacleSpawner>().GetSpeed());
    }

    private IEnumerator IndicatorSizeIncrease()
    {
        for (int i = 0; i < 25; i++)
        {
            if(transform.localScale.x <= 0.7f)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f);
            }
            else
            {
                transform.localScale *= 1.03f;
            }
            yield return new WaitForSeconds(0.002f);
        }
    }

    public void DestroyDeathIndicator()
    {
        Destroy(gameObject);
    }
}
