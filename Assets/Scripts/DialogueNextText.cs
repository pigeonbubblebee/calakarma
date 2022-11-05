using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueNextText : MonoBehaviour
{
    public TextMeshProUGUI keybindText;

    void Awake()
    {
        if(ControlBinds.GetBindMap("Dialogue").isKey) {
                if(ControlBinds.GetBindMap("Dialogue").key.Equals(KeyCode.LeftShift)) {
                    keybindText.text =  LocalizationSystem.getLocalizedValue("shift:key") + " ->";
                } else if(ControlBinds.GetBindMap("Dialogue").key.Equals(KeyCode.Escape)) {
                    keybindText.text = LocalizationSystem.getLocalizedValue("escape:key") + " ->";
                } else if(ControlBinds.GetBindMap("Dialogue").key.Equals(KeyCode.Space)) {
                    keybindText.text = LocalizationSystem.getLocalizedValue("space:key") + " ->";
                } else {
                    keybindText.text = ControlBinds.GetBindMap("Dialogue").key.ToString() + " ->";
                }
            } else {
                bool x = false;

                switch(ControlBinds.GetBindMap("Dialogue").mouseButton) {
                    case 0:
                        keybindText.text = "LMB ->";
                        x = true;
                        break;
                    case 1:
                        keybindText.text = "RMB ->";
                        x = true;
                        break;
                }

            if(!x)
                keybindText.text = "MB" + ControlBinds.GetBindMap("Dialogue").mouseButton + " ->";
        }
    }
}
