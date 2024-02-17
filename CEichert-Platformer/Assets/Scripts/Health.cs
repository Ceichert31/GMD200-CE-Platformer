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

    [Header("Current Scene")]
    [SerializeField] private int currentScene;

    public delegate void DamageHandler();
    public static DamageHandler takeDamage;

    private void Start()
    {
        player = GetComponentInChildren<SpriteRenderer>();
    }
    void TakeDamage()
    {
        if (!canDamage) return;

        health--;
        canDamage = false;

        if (health < 0)
            SceneLoader.load?.Invoke(currentScene);

        StartCoroutine(DamageTaken(iFrames));
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
