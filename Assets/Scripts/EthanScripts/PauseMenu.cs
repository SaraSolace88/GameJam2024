using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject SettingsPanel;

    public bool IsPaused;
    public Toggle FullScreen;
    public Toggle Vsync;

    public List<ResolutionItem> Resolutions = new List<ResolutionItem>();
    private int SelectedRes;
    public TextMeshProUGUI ResSettingDisplay;

    public bool IsLevel;
    private double pauseStartTime;
    private double audioPausedTime;

    private Controls pInput;

    private void Start()
    {
        FullScreen.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            Vsync.isOn = false;
        }
        else
        {
            Vsync.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < Resolutions.Count; i++)
        {
            if (Screen.width == Resolutions[i].Horizontal && Screen.height == Resolutions[i].Vertical)
            {
                foundRes = true;

                SelectedRes = i;
            }
        }

        if (!foundRes)
        {
            ResolutionItem newRes = new ResolutionItem();
            newRes.Horizontal = Screen.width;
            newRes.Vertical = Screen.height;
            Resolutions.Add(newRes);
            SelectedRes = Resolutions.Count - 1;
        }
    }

    private void OnEnable()
    {
        pInput = new Controls();
        pInput.Enable();
        pInput.Player.Pause.performed += ButtonPressed;
    }
    private void OnDisable()
    {
        pInput.Player.Pause.performed -= ButtonPressed;
    }
     private void ButtonPressed(InputAction.CallbackContext c)
    {
        if (IsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    private void OnGUI()
    {
        ResSettingDisplay.text = Resolutions[SelectedRes].Horizontal.ToString() + " X " + Resolutions[SelectedRes].Vertical.ToString();
    }
    public void Pause()
    {
        pauseStartTime = AudioSettings.dspTime;
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void End()
    {
        pauseStartTime = AudioSettings.dspTime;
        Time.timeScale = 0f;
        IsPaused = true;
    }
    public void Resume()
    {
        double pauseDuration = AudioSettings.dspTime - pauseStartTime;
        audioPausedTime += pauseDuration;

        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public float GetAdjustedAudioTime()
    {
        return (float)AudioSettings.dspTime - (float)audioPausedTime;
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        SettingsPanel.SetActive(false);
    }

    public void ResLeft()
    {
        SelectedRes--;
        if (SelectedRes < 0)
        {
            SelectedRes = 0;
        }
    }
    public void ResRight()
    {
        SelectedRes++;
        if (SelectedRes > Resolutions.Count - 1)
        {
            SelectedRes = Resolutions.Count - 1;
        }
    }
    public void ApplyGraphics()
    {
        if (Vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(Resolutions[SelectedRes].Horizontal, Resolutions[SelectedRes].Vertical, FullScreen.isOn);
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int Horizontal;
    public int Vertical;
}
