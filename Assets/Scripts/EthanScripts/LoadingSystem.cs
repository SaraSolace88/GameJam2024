using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSystem : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject Main;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider LoadingBar;

    // The index into RhythmSystem.song_prefabs.
    // This must be overwritten by a button before LoadSceneLoading is called.
    private int song_to_play = -1;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetSongId(int song_id) {
        song_to_play = song_id;
    }
    
    public void LoadSceneLoading(string LevelToLoad)
    {
        Main.SetActive(false);
        LoadingScreen.SetActive(true);

        SavingSong(song_to_play);
        StartCoroutine(LoadSceneAsync(LevelToLoad));
        Time.timeScale = 1;
    }


    void SavingSong(int song) {
        PlayerPrefs.SetInt("song_id", song);
    }

    IEnumerator LoadSceneAsync(string LevelToLoad)
    {
        AsyncOperation loadOperatioin = SceneManager.LoadSceneAsync(LevelToLoad);
        while (!loadOperatioin.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperatioin.progress / 0.9f);
            LoadingBar.value = progressValue;
            yield return null;
        }
    }
}
