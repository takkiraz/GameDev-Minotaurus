using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private static int maxScene = 1;

    void Awake()
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


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index % maxScene);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNext()
    {
        LoadScene((SceneManager.GetActiveScene().buildIndex + 1 + maxScene) % maxScene);
    }

    public void LoadPrev()
    {
        LoadScene((SceneManager.GetActiveScene().buildIndex - 1 + maxScene) % maxScene);
    }

    public int BuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxScene = SceneManager.sceneCountInBuildSettings;
    }

}
