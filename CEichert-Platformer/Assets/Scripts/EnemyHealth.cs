using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;

    private Animator animator;

    private EnemyFire fire;

    private void Start()
    {
        animator = GetComponent<Animator>();

        fire = GetComponent<EnemyFire>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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

                health--;

                if (health <= 0)
                    Destroy(gameObject);

                animator.SetTrigger("Hurt");
                ShootEnable();
            }
            else
                Health.takeDamage?.Invoke();
        }
    }

    public void ShootEnable()
    {
        fire.enabled = !fire.enabled;
    }
}
