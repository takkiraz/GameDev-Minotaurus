using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> musics = default;
    public static MusicManager instance;

    float maxVol = 0.7f;
    int currentMusicIndex = 0;
    AudioSource player = default;


    public IEnumerator FadeOut(float time)
    {

        while (player.volume > 0)
        {
            player.volume -= 1f * Time.deltaTime / time;
            yield return null;
        }
        player.Stop();
    }

    public IEnumerator FadeIn(float time)
    {
        player.Play();
        while (player.volume < maxVol)
        {
            player.volume += 1f * Time.deltaTime / time;
            yield return null;
        }
    }

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
        player = GetComponent<AudioSource>();
        player.clip = musics[0];
        player.Play();
    }

    public void Next()
    {
        currentMusicIndex = (currentMusicIndex + 1 + musics.Count) % musics.Count;
        Play(currentMusicIndex);
    }

    public void Prev()
    {
        currentMusicIndex = (currentMusicIndex - 1 + musics.Count) % musics.Count;
        Play(currentMusicIndex);
    }

    public void PlayShot(int index)
    {
        int shotIndex = index % musics.Count;
        player.PlayOneShot(musics[shotIndex], 1f);
    }

    public void Play(int index, float time = 1.0f)
    {
        currentMusicIndex = index % musics.Count;
        StopAllCoroutines();
        StartCoroutine(FadeOut(time));
        player.clip = musics[currentMusicIndex];
        StartCoroutine(FadeIn(time));
    }

    public void Stop(float time = 1.0f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(time));
    }


}