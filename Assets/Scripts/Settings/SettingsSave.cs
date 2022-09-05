using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SettingsSave : MonoBehaviour
{
    public static bool autoSave;
    public static string language;

    public static string[] bindNames;
    public static KeyCode[] bindKeys;
    public static int[] mouseButtons;
    public static bool[] isMouseButtons;

    public static int resolutionW;
    public static int resolutionH;
    public static bool fullscreen;
    public static bool postProcess;

    public static int masterVolume;
    public static int soundEffects;
    public static int music;

    public static void Save() {
        PlayerPrefs.SetInt("autoSave", autoSave ? 1 : 0);
        PlayerPrefs.SetString("language", language);

        bindNames = ControlBinds.getDefaultActions();

        for(int i = 0; i < bindNames.Length; i++) {
            string x = bindNames[i];
            Bind b = ControlBinds.GetBindMap(x);

            bindKeys[i] = b.key;
            mouseButtons[i] = b.mouseButton;
            isMouseButtons[i] = !b.isKey;
        }

        int count = 0;

        foreach(string s in bindNames) {
            PlayerPrefs.SetString("bindKey:" + s, bindKeys[count].ToString());
            PlayerPrefs.SetInt("bindMouseButton:" + s, mouseButtons[count]);
            PlayerPrefs.SetInt("bindIsMouse:" + s, isMouseButtons[count] ? 1 : 0);

            count++;
        }

        PlayerPrefs.SetInt("resolutionW", resolutionW);
        PlayerPrefs.SetInt("resolutionH", resolutionH);
        PlayerPrefs.SetInt("fullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.SetInt("postProcess", postProcess ? 1 : 0);

        PlayerPrefs.SetInt("masterVolume", masterVolume);
        PlayerPrefs.SetInt("soundEffects", soundEffects);
        PlayerPrefs.SetInt("music", music);
    }

    public static void Load() {
        autoSave = (PlayerPrefs.GetInt("autoSave", 1) == 1) ? true : false;
        language = PlayerPrefs.GetString("language", "en");

        int count = 0;

        bindNames = ControlBinds.getDefaultActions();
        bindKeys = new KeyCode[ControlBinds.getDefaultActions().Length];
        mouseButtons = new int[ControlBinds.getDefaultActions().Length];
        isMouseButtons = new bool[ControlBinds.getDefaultActions().Length];

        foreach(string s in bindNames) {
            bindKeys[count] = (KeyCode)Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("bindKey:"  + s, ControlBinds.GetBindMap(s).key.ToString()) );
            mouseButtons[count] = PlayerPrefs.GetInt("bindMouseButton:" + s, ControlBinds.GetBindMap(s).mouseButton);
            isMouseButtons[count] = (PlayerPrefs.GetInt("bindIsMouse:" + s, ControlBinds.GetBindMap(s).isKey ? 0 : 1) == 1) ? true : false;

            Bind result;

            if(isMouseButtons[count]) {
                result = new Bind(mouseButtons[count]);
            } else {
                result = new Bind(bindKeys[count]);
            }

            ControlBinds.SetBindMap(s, result);

            count++;
        }

        resolutionW = PlayerPrefs.GetInt("resolutionW", 1600);
        resolutionH = PlayerPrefs.GetInt("resolutionH", 900);
        fullscreen = (PlayerPrefs.GetInt("fullscreen", 0) == 1) ? true : false;
        postProcess = (PlayerPrefs.GetInt("postProcess", 1) == 1) ? true : false;

        masterVolume = PlayerPrefs.GetInt("masterVolume", 100);
        soundEffects = PlayerPrefs.GetInt("soundEffects", 100);
        music = PlayerPrefs.GetInt("music", 100);
    }

    public static Bind[] GetBinds() {
        Bind[] result = new Bind[bindNames.Length];
        for(int i = 0; i < bindNames.Length; i++) {
            if(isMouseButtons[i]) {
                result[i] = new Bind(mouseButtons[i]);
            } else {
                result[i] = new Bind(bindKeys[i]);
            }
        }
        return result;
    }
}
