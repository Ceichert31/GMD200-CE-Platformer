using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [Header("Firing Settings")]
    [SerializeField] private GameObject bullet;

    [Tooltip("The amount of time that needs to pass to fire another bullet")]
    [SerializeField] private float fireRate = 3f;

    [Tooltip("The number of bullets that is fired per shot")]
    [SerializeField] private int bulletNumber = 3;

    [Tooltip("The pause between each bullet shot")]
    [SerializeField] private float pauseBetweenBullets = 0.1f;

    [Tooltip("The position bullets are instantiated from")]
    [SerializeField] private Transform spawningPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire(bulletNumber, pauseBetweenBullets, fireRate));
    }

    IEnumerator Fire(int bulletNumber, float pauseBetweenBullets, float firingDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            Instantiate(bullet, spawningPosition.transform.position, bullet.transform.rotation);
            yield return new WaitForSeconds(pauseBetweenBullets);
        }
        yield return new WaitForSeconds(firingDelay);
        StartCoroutine(Fire(bulletNumber, pauseBetweenBullets, fireRate));
    }
}
