using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            InputManager player = collision.gameObject.GetComponent<InputManager>();
            player.Bounce(20);
            Bullet instance = gameObject.GetComponentInParent<Bullet>();
            instance.gameObject.layer = 0;
            Destroy(gameObject);
        }
    }
}
