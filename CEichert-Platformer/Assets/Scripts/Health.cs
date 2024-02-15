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
    
    public delegate void DamageHandler();
    public static DamageHandler takeDamage;

    private void Start()
    {
        player = GetComponent<SpriteRenderer>();
    }
    void TakeDamage()
    {
        if (!canDamage) return;

        health--;

        if (health < 0)
            Destroy(gameObject);

        StartCoroutine(DamageTaken(iFrames));
    }
    private void Update()
    {
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

    private void OnEnable()
    {
        takeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        takeDamage -= TakeDamage;
    }
}
