using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentalDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Health.takeDamage?.Invoke();
            InputManager managerInstance = collision.gameObject.GetComponent<InputManager>();
            managerInstance.Bounce(15);
        }
            
    }
}
