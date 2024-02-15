using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector2 direction;

    [Header("Bullet Settings")]
    [Tooltip("Speed of the bullet")]
    [SerializeField] private float bulletSpeed = 5f;

    [Tooltip("How long the bullet will exsist until it is destroyed")]
    [SerializeField] private float lifeTime = 6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb.velocity = bulletSpeed * direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 6)
        {
            //Get direction the player is colliding with the bullet
            Vector3 playerInstance = collision.gameObject.transform.position;
            Vector2 direction = (transform.position - playerInstance).normalized;
            float dot = Vector2.Dot(Vector2.up, direction);
            //If direction is negative, bounce player
            if (Mathf.Sign(dot) == -1)
            {
                InputManager inputManager = collision.gameObject.GetComponent<InputManager>();
                inputManager.Bounce(15);
            }
            else
                Health.takeDamage?.Invoke();
        }
    }

}
