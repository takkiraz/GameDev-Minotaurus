using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { HEAL, TIME, KILL };
public class Item : MonoBehaviour
{

    private SpriteRenderer childImage = default;
    [SerializeField] ItemType type = default;
    [SerializeField] List<Sprite> imageArrays = default;

    private void Start()
    {
        childImage = GetComponent<SpriteRenderer>();

        // Get Random PowerUp
        switch (Random.Range(0, 3))
        {
            case 0:
                type = ItemType.HEAL;
                childImage.sprite = imageArrays[0];
                break;
            case 1:
                type = ItemType.TIME;
                childImage.sprite = imageArrays[1];
                break;
            default:
                type = ItemType.KILL;
                childImage.sprite = imageArrays[2];
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == ItemType.HEAL)
                collision.SendMessageUpwards("Healing", Random.Range(70, 150));
            if (type == ItemType.TIME)
                GameManager.instance.AddTime(Random.Range(5, 15));
            if (type == ItemType.KILL)
                GameManager.instance.AddKills(Random.Range(1, 4));

            Destroy(transform.parent.gameObject, 0.3f);
        }
    }
}
