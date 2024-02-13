using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int health = 3;
    [SerializeField] private int iFrames = 6;

    private SpriteRenderer player;

    private bool canDamage = true;

    private void Start()
    {
        player = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDamage)
            return;

        if (collision.gameObject.layer == 7)
        {
            canDamage = false;
            health--;
            StartCoroutine(DamageTaken(iFrames));
        }
            

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageTaken(int iFrames)
    {
        for (int i = 0; i < iFrames; i++)
        {
            player.enabled = !player.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        canDamage = true;
    }
}
