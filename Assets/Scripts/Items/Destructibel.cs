using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibel : MonoBehaviour
{
    private int health = 40;
    private Animator anim;
    private GameObject item;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        item = transform.Find("Food").gameObject;
        sr = GetComponent<SpriteRenderer>();

        item.SetActive(false);
    }
    

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine("Destroy");
        }
        anim.SetTrigger("barrelHitTrigger");
    }

    private IEnumerator Destroy()
    {
        anim.SetBool("barrelDestroy", true);

        yield return new WaitForSeconds(0.7f);

        sr.enabled = false;
        item.SetActive(true);
    }
}
