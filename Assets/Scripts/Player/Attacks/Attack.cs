using UnityEngine;
using System.Collections;

// Base Class for Player Attacks

public class Attack : MonoBehaviour
{
    protected int damage { get; set; } = 10;

    private Collider2D col;
    protected bool isFacingRight = true;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Flip the Collider if Player changes direction

        if (!transform.parent.gameObject.GetComponent<PlayerController>().isFacingRight && isFacingRight)
        {
            Flip();
        }

        if (transform.parent.gameObject.GetComponent<PlayerController>().isFacingRight && !isFacingRight)
        {
            Flip();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Destructibel"))
        {
            collision.SendMessageUpwards("Damage", damage);
        }
    }

    // Flips the Attack Colliders

    protected void Flip()
    {
        col.offset = new Vector2(-1 * col.offset.x, col.offset.y);

        isFacingRight = !isFacingRight;
    }
}
