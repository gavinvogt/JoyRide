using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class AirStrikeSpecial : SpecialMoveBase
{
    [SerializeField] private float explosionRange;
    [SerializeField] private float secondsBetweenBursts;
    [SerializeField] private int numOfExplosionsPerBurst;
    [SerializeField] private int numOfExplosionBursts;
    [SerializeField] private int explosionDamage;
    [SerializeField] GameObject explosion;
    [SerializeField] private AudioClip hitSound;

    protected Camera mainCamera;
    private Vector3 mousePos;

    private bool isActive = false;
    private ArrayList explosions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        explosions = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateSpecialMove()
    {
        if (isActive) return; // ignore extra special activations
        // TODO: add some special cooldown, with indicator in game UI

        isActive = true;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        StartCoroutine(SpawnExplosion(0));
    }

    private IEnumerator SpawnExplosion(int numExplosionsFired)
    {
        if (numExplosionsFired < numOfExplosionBursts)
        {
            numExplosionsFired++;
            yield return new WaitForSeconds(secondsBetweenBursts);
            SoundFXManager.instance.PlaySoundFXClip(hitSound, transform, 1f);
            for (int i = 1; i < numOfExplosionBursts; i++)
            {
                SpawnExplosionObject();
            }
            StartCoroutine(SpawnExplosion(numExplosionsFired));
        }
        else
        {
            EndSpecialMove();
        }
    }

    private void SpawnExplosionObject()
    {
        float randForAngle = Random.Range(-Mathf.PI, Mathf.PI) * Mathf.Rad2Deg;
        float randForRadius = explosionRange * Mathf.Sqrt(Random.Range(0f, 1f));
        Vector3 spawnPosition = new Vector3(mousePos.x + randForRadius * Mathf.Cos(randForAngle), mousePos.y + randForRadius * Mathf.Sin(randForAngle), -6.1f);
        GameObject explosionCopy = Instantiate(explosion, spawnPosition, transform.rotation);
        explosionCopy.GetComponent<RocketGunExplosion>().SetDamage(explosionDamage);
        explosions.Add(explosionCopy);
        StartCoroutine(ExplosionSizeIncrease(explosionCopy));
        StartCoroutine(DestroyExplosion(explosionCopy));
    }

    private IEnumerator ExplosionSizeIncrease(GameObject explosion)
    {
        explosion.transform.localScale *= 0.5f;
        for (int i = 0; i < (int)(secondsBetweenBursts * 100f); i++)
        {
            explosion.transform.localScale *= 1.07f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator DestroyExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(secondsBetweenBursts * 1.33f);
        explosions.Remove(explosion);
        Destroy(explosion);
    }

    public override void EndSpecialMove()
    {
        isActive = false;
    }

    public void OnDestroy()
    {
        //Make list of all explosions to delete here
        if (isActive)
        {
            EndSpecialMove();
        }
        foreach (GameObject explosion in explosions)
        {
            Destroy(explosion);
        }
    }
}
