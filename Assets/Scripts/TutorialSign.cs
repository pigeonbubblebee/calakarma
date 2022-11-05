using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSign : MonoBehaviour
{
    public string control;
    public TextMeshPro tmp;

    void Awake()
    {
        if(ControlBinds.GetBindMap(control).isKey) {
                if(ControlBinds.GetBindMap(control).key.Equals(KeyCode.LeftShift)) {
                    tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " " + LocalizationSystem.getLocalizedValue("shift:key") + " " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                } else if(ControlBinds.GetBindMap(control).key.Equals(KeyCode.Escape)) {
                    tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " " + LocalizationSystem.getLocalizedValue("escape:key") + " " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                } else if(ControlBinds.GetBindMap(control).key.Equals(KeyCode.Space)) {
                    tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " " + LocalizationSystem.getLocalizedValue("space:key") + " " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                } else {
                    tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " " + ControlBinds.GetBindMap(control).key.ToString() + " " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                }
            } else {
                bool x = false;

                switch(ControlBinds.GetBindMap(control).mouseButton) {
                    case 0:
                        tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " LMB " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                        x = true;
                        break;
                    case 1:
                        tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " RMB " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
                        x = true;
                        break;
                }

            if(!x)
                tmp.text = tmp.text = LocalizationSystem.getLocalizedValue("use:tutorialtext") + " MB" +  + ControlBinds.GetBindMap(control).mouseButton + " " + LocalizationSystem.getLocalizedValue(control+":tutorialtext");
        }
    }
}
