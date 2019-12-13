using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject player = default;
    public static HealthBar instance = null;
    private Image img = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        img = GetComponent<Image>();
    }

    public void UpdateBar(float normalized)
    {
        img.fillAmount = normalized;
    }
}
