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

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadSceneLoading(string LevelToLoad)
    {
        Main.SetActive(false);
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsync(LevelToLoad));
        Time.timeScale = 1;
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
