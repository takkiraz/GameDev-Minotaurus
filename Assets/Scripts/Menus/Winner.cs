using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public void OnPlayAgain()
    {
        MusicManager.instance.PlayShot(2);
        GameManager.currLvl = 0;
        SceneLoader.instance.LoadScene(1);
    }

    public void BackToMenu()
    {
        MusicManager.instance.PlayShot(2);
        GameManager.currLvl = 0;
        SceneLoader.instance.LoadScene(0);
    }

    public void Quit()
    {
        MusicManager.instance.PlayShot(2);
        Application.Quit();
    }

    public void PlayRoll()
    {
        MusicManager.instance.PlayShot(3);
    }

    public void Click()
    {
        MusicManager.instance.PlayShot(2);
    }
}
