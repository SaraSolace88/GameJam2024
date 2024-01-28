using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private GameObject TutorialPanel;

    public bool IsTutorial;
    public bool Run;

    public void Awake()
    {
        if(PlayerPrefs.HasKey("Tut"))
        {
            Run = false;
        }
    }
    public void Start()
    {
        if (Run)
        {
            if (!PlayerPrefs.HasKey("TUT"))
            {
                PlayerPrefs.SetInt("TUT", 1);
                TutorialPanel.SetActive(true);
                Time.timeScale = 0f;
                IsTutorial = true;
            }
            else
            {
                return;
            }
        }
    }

    public void Close()
    {
        TutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        IsTutorial = false;
    }
}
