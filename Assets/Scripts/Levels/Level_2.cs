using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level_2 : MonoBehaviour
{

    [SerializeField] Text timer = default;
    [SerializeField] Text killCounter = default;
    [SerializeField] GameObject nextLevelScene = default;

    void Start()
    {
        timer.text = 60.ToString();
        GameManager.timer = timer;
        GameManager.killCounter = killCounter;
        GameManager.nextLevelScene = nextLevelScene;
        GameManager.currLvl = 1;
        GameManager.instance.StartTimer();
    }

}
