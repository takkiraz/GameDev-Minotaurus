using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static Text timer = default;
    public static Text killCounter = default;
    public static GameObject nextLevelScene = null;
    public static int currLvl = 0;
    public bool isFinished = false;

    string text = "Kills to Win:  ";

    int[] killsToWin = { 6, 8, 10 };
    int kills = 0;

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

        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if(currLvl < 3)
        {
            RenderKills();
        }
    }

    // Shows next level Scene or loads the next level
    IEnumerator LevelDone()
    {
        isFinished = true;
        yield return new WaitForSeconds(0.8f);

        if (nextLevelScene != null)
        {
            nextLevelScene.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        // Game finished
        if (currLvl > 2)
        {
            StopCoroutine(Timer());
            MusicManager.instance.Play(7);
        }
        else
        {
            SceneLoader.instance.LoadNext();
        }
    }

    public void StartTimer()
    {
        isFinished = false;
        kills = 0;
        StopAllCoroutines();
        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        int x = int.Parse(timer.text);
        x--;
        timer.text = x.ToString();
        if (x > 0)
        {
            yield return StartCoroutine(Timer());
        }
        else
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        isFinished = true;
        StopAllCoroutines();
        SceneLoader.instance.LoadScene("GameOver");
    }

    public void RenderKills() {
        int killsLeft = killsToWin[currLvl] - kills;

        if (killCounter != null && !isFinished)
        {
            killCounter.text = text + killsLeft.ToString();
        }
        // Player wins the level
        if (killsLeft <= 0 && !isFinished)
        {
            isFinished = true;
            StopAllCoroutines();
            currLvl++;
            StartCoroutine(LevelDone());
        }
    }

    // Called by the Enemy

    public void Kill()
    {
        kills++;
    }


    // Functions for Items

    public void AddTime(int extraTime)
    {
        timer.text = (int.Parse(timer.text) + extraTime).ToString();
    }
    public void AddKills(int extraKill)
    {
        kills += extraKill;
    }
}