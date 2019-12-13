using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private BoxCollider2D[] colliders;
    private Color orgColor;
    public bool isFacingRight = true;
    private bool isDead = false;
    public int speed = 7;

    public int maxHealth = 200;
    private int health = 200;
    private bool gettingDamage = false;
    public bool CameraIsAnimated = false;

    private void Healing(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        HealthBar.instance.UpdateBar((float)health / maxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren<BoxCollider2D>();
        orgColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack1");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("attack2");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CameraIsAnimated = true;
                StopCoroutine(EarthQuake());
                anim.SetTrigger("attack3");
                StartCoroutine(EarthQuake());
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CameraIsAnimated = true;
                StopCoroutine(ZoomOut());
                anim.SetTrigger("attack4");
                StartCoroutine(ZoomOut());
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("taunt", true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("taunt", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            anim.SetFloat("axisHorizontal", horizontal);
            Vector2 movement = new Vector2(horizontal, 0);

            if (horizontal < 0 && isFacingRight)
            {
                Flip();
            }

            if (horizontal > 0 && !isFacingRight)
            {
                Flip();
            }

            if (!gettingDamage)
            {
                rb.velocity = movement * speed;

                if (System.Math.Abs(Input.GetAxis("Horizontal")) < 0.05)
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    // Flip X - Axis from Player and Collider
    private void Flip()
    {
        foreach (BoxCollider2D col in colliders)
            col.offset = new Vector2(-1 * col.offset.x, col.offset.y);

        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void Damage(int damage)
    {
        // if level is not finished, player can get damage
        if (!GameManager.instance.isFinished)
        {
            health -= damage;

            // Give the player a kick back
            gettingDamage = true;
            rb.AddForce((isFacingRight ? Vector2.left : Vector2.right) * 0.6f, ForceMode2D.Impulse);

            StartCoroutine(ResetVelocity());

            if (health < 0) health = 0;
            MusicManager.instance.PlayShot(6);

            // Update Healthbar
            float normalized = (float)health / maxHealth;
            HealthBar.instance.UpdateBar(normalized);

            if (health == 0)
            {
                Die();
            }
            else if (health <= 50 && health > 0)
            {
                StopCoroutine(HitAnimation());
                anim.SetTrigger("damage2");
                StartCoroutine(HitAnimation());
            }
            else if (health > 50)
            {
                StopCoroutine(HitAnimation());
                anim.SetTrigger("damage1");
                StartCoroutine(HitAnimation());
            }
        }
    }

    private IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(0.3f);
        gettingDamage = false;
        rb.velocity = (isFacingRight ? Vector2.left : Vector2.right) * 0.1f;
    }

    private void Die()
    {
        StopCoroutine(ResetVelocity());
        rb.velocity = Vector3.zero;
        if (!isDead)
        {
            anim.SetBool("dead", true);
            isDead = true;
            GameManager.instance.GameOver();
            foreach (Collider2D col in colliders)
                col.enabled = false;

        }
        StopAllCoroutines();
    }

    // EarthQuake effect on Stamp Attack(3)

    private IEnumerator EarthQuake()
    {
        GameObject.FindWithTag("CameraCurrent").transform.position += Vector3.up * 0.5f;
        yield return new WaitForSeconds(0.1f);
        GameObject.FindWithTag("CameraCurrent").transform.position += Vector3.left * 0.2f;
        yield return new WaitForSeconds(0.1f);
        GameObject.FindWithTag("CameraCurrent").transform.position += Vector3.down * 1f;
        yield return new WaitForSeconds(0.1f);
        GameObject.FindWithTag("CameraCurrent").transform.position += Vector3.right * 0.2f;
        yield return new WaitForSeconds(0.1f);
        GameObject.FindWithTag("CameraCurrent").transform.position += Vector3.up * 0.5f;
        CameraIsAnimated = false;
    }

    // Zoom effect on Attack 4

    private IEnumerator ZoomOut()
    {
        Camera cam = GameObject.FindWithTag("CameraCurrent").GetComponent<Camera>();
        yield return new WaitForSeconds(0.1f);
        cam.orthographicSize = 4.5f;
        yield return new WaitForSeconds(0.05f);
        cam.orthographicSize = 5f;
        yield return new WaitForSeconds(0.05f);
        cam.orthographicSize = 5.5f;
        yield return new WaitForSeconds(0.05f);
        cam.orthographicSize = 6f;
        yield return new WaitForSeconds(0.05f);
        cam.orthographicSize = 5.5f;
        yield return new WaitForSeconds(0.05f);
        cam.orthographicSize = 5;
        CameraIsAnimated = false;
    }

    private IEnumerator HitAnimation()
    {
        Color flash = orgColor;
        flash.a = 0.3f;

        yield return new WaitForSeconds(0.4f);

        spriteRenderer.color = flash;
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = orgColor;
        yield return new WaitForSeconds(0.4f);
    }
}
