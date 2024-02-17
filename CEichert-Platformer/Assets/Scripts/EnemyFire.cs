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

    private Animator animator;

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = fireRate;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        waitTime -= Time.deltaTime;

        if (waitTime <= fireRate / 2)
            animator.SetTrigger("Charged");

        if (waitTime <= 0)
        {
            StartCoroutine(Fire(bulletNumber, pauseBetweenBullets, fireRate));
            waitTime = fireRate;
            animator.SetTrigger("Shoot");
        }

    }
    IEnumerator Fire(int bulletNumber, float pauseBetweenBullets, float firingDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            Bullet instance = Instantiate(bullet, spawningPosition.transform.position, spawningPosition.rotation).GetComponent<Bullet>();
            instance.direction = -spawningPosition.right;
            yield return new WaitForSeconds(pauseBetweenBullets);
        }
        yield return new WaitForSeconds(firingDelay);
    }
}
