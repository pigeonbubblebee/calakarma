using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        SettingsSave.Load();

        Screen.SetResolution(SettingsSave.resolutionW, SettingsSave.resolutionH, SettingsSave.fullscreen);
    }

    public void Play() {
        SceneManager.LoadScene("Save_Slots");
    }

    public void Resume() {
        UIManager.Instance.toggleUIOff(UIManager.EscMenu);
    }


    public void Settings() {
        SettingsFunctionality.loadScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Settings");
    }

    public void Quit() {
        Application.Quit();
    }
}
