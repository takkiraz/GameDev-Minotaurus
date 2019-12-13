using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlayGame()
    {
        MusicManager.instance.PlayShot(2);
        StartCoroutine(PlayLevel1());
    }

    public void Quit()
    {
        MusicManager.instance.PlayShot(2);
        Application.Quit();
    }

    private IEnumerator PlayLevel1()
    {
        yield return new WaitForSeconds(1.5f);
        SceneLoader.instance.LoadNext();
    }

    private void Start()
    {
        MusicManager.instance.Play(0);
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
