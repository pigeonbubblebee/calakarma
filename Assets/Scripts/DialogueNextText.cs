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
                    keybindText.text = "Shift ->";
                } else if(ControlBinds.GetBindMap("Dialogue").key.Equals(KeyCode.Escape)) {
                    keybindText.text = "Escape ->";
                } else if(ControlBinds.GetBindMap("Dialogue").key.Equals(KeyCode.Space)) {
                    keybindText.text = "Space ->";
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
