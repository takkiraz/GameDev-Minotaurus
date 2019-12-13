using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level_1 : MonoBehaviour
{

    [SerializeField] Text timer = default;
    [SerializeField] Text killCounter = default;
    [SerializeField] GameObject nextLevelScene = default;

    void Start()
    {
        MusicManager.instance.Play(1);
        timer.text = 45.ToString();
        GameManager.timer = timer;
        GameManager.killCounter = killCounter;
        GameManager.nextLevelScene = nextLevelScene;
        GameManager.currLvl = 0;
        GameManager.instance.StartTimer();
    }

}
