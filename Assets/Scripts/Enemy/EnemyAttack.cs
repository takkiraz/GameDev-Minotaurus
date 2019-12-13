using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public EnemyAttack()
    {
        damage = 15;
    }

    void Update()
    {
        // Flip the Collider if Enemy changes direction

        if (!transform.parent.gameObject.GetComponent<EnemyController>().isFacingRight && !base.isFacingRight)
        {
            base.Flip();
        }

        if (transform.parent.gameObject.GetComponent<EnemyController>().isFacingRight && base.isFacingRight)
        {
            base.Flip();
        }
    }

    // Override Base OnTriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", damage);
        }
    }
}
