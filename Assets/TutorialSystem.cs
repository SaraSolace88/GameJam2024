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
        if (Run)
        {
            TutorialPanel.SetActive(true);
            Time.timeScale = 0f;
            IsTutorial = true;
        }
    }

    public void Close()
    {
        TutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        IsTutorial = false;
    }
}
