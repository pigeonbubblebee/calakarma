using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsLoad : MonoBehaviour
{
    public void load(int x) {
        Save.currentSaveSlot = x;
        Save.LoadScene();
        string scene = Save.getScene();

        if(scene == "" || scene == null) {
            SceneManager.LoadScene("Outskirts_1");
        } else {
            SceneManager.LoadScene(scene);
        }
    }
}
