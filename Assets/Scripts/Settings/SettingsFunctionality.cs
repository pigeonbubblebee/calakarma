
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsFunctionality : MonoBehaviour
{
    public Toggle autoSave;
    public TMP_Dropdown language;

    private int currentBindIndex;
    private bool isBinding = true;

    private bool isMouseDown = false;

    public TextMeshProUGUI[] keybindText;

    public TMP_Dropdown resolution;
    public Toggle fullscreen;
    public Toggle postProcess;

    public Slider master;
    public Slider music;
    public Slider sfx;

    public GameObject[] tabs;

    public static string loadScene;

    public void setTab(int tab) {
        foreach(GameObject x in tabs) {
            x.SetActive(false);
        }

        tabs[tab].SetActive(true);
    }

    public void setAutoSave() {
        SettingsSave.autoSave = autoSave.isOn;
    }

    void OnApplicationQuit()
    {
        SettingsSave.Save();
    }

    public void setLanguage(string lan) {
        SettingsSave.language = lan;

        switch(lan) {
            case "en":
                LocalizationSystem.language = LocalizationSystem.Language.English;
                break;
        }
    }

    public void setBind(int bind) {
        currentBindIndex = bind;
        isBinding = true;
        isMouseDown = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0)) {
            isMouseDown = false;
        }
        if(ControlBinds.GetButtonDown("Escape")) {
            SettingsSave.Save();
            SceneManager.LoadScene(loadScene);
        }
    }

    void Awake()
    {
        SettingsSave.Load();

        autoSave.isOn = SettingsSave.autoSave;

        switch(SettingsSave.language) {
            case "en":
                language.value = 0;
                break;
        }

        string[] action = ControlBinds.getDefaultActions();

        isBinding = false;
        for(int i = 0; i < keybindText.Length; i++) {
            if(ControlBinds.GetBindMap(action[i]).isKey) {
                if(ControlBinds.GetBindMap(action[i]).key.Equals(KeyCode.LeftShift)) {
                    keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - Shift";
                } else if(ControlBinds.GetBindMap(action[i]).key.Equals(KeyCode.Escape)) {
                    keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - Escape";
                } else if(ControlBinds.GetBindMap(action[i]).key.Equals(KeyCode.Space)) {
                    keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - Space";
                } else {
                    keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - " + ControlBinds.GetBindMap(action[i]).key.ToString();
                }
            } else {
                bool x = false;

                switch(ControlBinds.GetBindMap(action[i]).mouseButton) {
                    case 0:
                        keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - LMB";
                        x = true;
                        break;
                    case 1:
                        keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - RMB";
                        x = true;
                        break;
                }

                if(!x)
                    keybindText[i].text = LocalizationSystem.getLocalizedValue(action[i]+":bind") + " - MB" + ControlBinds.GetBindMap(action[i]).mouseButton;
            }
        }

        if(SettingsSave.resolutionW == 1600 && SettingsSave.resolutionH == 900)
            resolution.value = 0;
        else if(SettingsSave.resolutionW == 800 && SettingsSave.resolutionH == 450)
            resolution.value = 1;

        fullscreen.isOn = SettingsSave.fullscreen;
        postProcess.isOn = SettingsSave.postProcess;

        master.value = ((float)(SettingsSave.masterVolume))/100;
        music.value = ((float)(SettingsSave.music))/100;
        sfx.value = ((float)(SettingsSave.soundEffects))/100;
    }

    void OnGUI()
    {
        if(isBinding && !isMouseDown) {
            if(Input.GetKey(KeyCode.LeftShift)) {
                string action = ControlBinds.getDefaultActions()[currentBindIndex];
                ControlBinds.SetBindMap(action, new Bind(KeyCode.LeftShift));
                isBinding = false;
                keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - Shift";
                return;
            }

            if(Input.GetKey(KeyCode.Space)) {
                string action = ControlBinds.getDefaultActions()[currentBindIndex];
                ControlBinds.SetBindMap(action, new Bind(KeyCode.Space));
                isBinding = false;
                keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - Space";
                return;
            }

            Event e = Event.current;
            if(e.isKey) {
                if(e.character!=0) {
                    string action = ControlBinds.getDefaultActions()[currentBindIndex];
                    try {
                        KeyCode kc = (KeyCode) System.Enum.Parse(typeof(KeyCode), Char.ToString(e.character).ToUpper());
                        ControlBinds.SetBindMap(action, new Bind(kc));
                        isBinding = false;
                        keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - " + kc.ToString();
                    } catch (ArgumentException) {
                        isBinding = false;
                    }
                }
            }
            if(e.isMouse) {
                string action = ControlBinds.getDefaultActions()[currentBindIndex];
                ControlBinds.SetBindMap(action, new Bind(e.button));
                isBinding = false;
                switch(e.button) {
                    case 0:
                        keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - LMB";
                        return;
                    case 1:
                        keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - RMB";
                        return;
                }
                keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - MB" + e.button;
                return;
            }

            if(Input.GetKey(KeyCode.Escape)) {
                string action = ControlBinds.getDefaultActions()[currentBindIndex];
                ControlBinds.SetBindMap(action, new Bind(KeyCode.Escape));
                isBinding = false;
                keybindText[currentBindIndex].text = LocalizationSystem.getLocalizedValue(action+":bind") + " - Escape";
            }
        }
    }

    public void setResolution(string WH) {
        string[] split = WH.Split(":");
        int width = Int32.Parse(split[0]);
        int height = Int32.Parse(split[1]);

        SettingsSave.resolutionW = width;
        SettingsSave.resolutionH = height;
    }

    public void setFullscreen() {
        SettingsSave.fullscreen = fullscreen.isOn;
    }

    public void setPostprocess() {
        SettingsSave.postProcess = postProcess.isOn;
    }

    public void applyRes() {
        Screen.SetResolution(SettingsSave.resolutionW, SettingsSave.resolutionH, SettingsSave.fullscreen);
    }

    public void setMaster() {
        SettingsSave.masterVolume = (int)(master.value*100);
    }

    public void setMusic() {
        SettingsSave.music = (int)(music.value*100);
    }

    public void setSFX() {
        SettingsSave.soundEffects = (int)(sfx.value*100);
    }
}
