using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tymski;
using UnityEngine.SceneManagement;

public class NodeExit : MonoBehaviour
{
    public string entranceName;
    public SceneReference scene;

    void onClick()
    {
        load();
    }

    void load() {
        Save.AutoSave();
        NodeManager.currentEntranceName = entranceName;

        SceneManager.LoadScene(scene);
    }

    // Changes Mouse Cursor When Mouse Goes Over
    void OnMouseEnter()
    {
        if(UIManager.Instance.uiOpen()) { // Stops Changing If UI is Open
            return;
        }
        UIManager.Instance.changeMouse(UIManager.Exit_Mouse);
    }

    // Changes Mouse Back When Mouse Leaves
    void OnMouseExit()
    {
        UIManager.Instance.changeMouse(UIManager.Default_Mouse);
    }

    // Checks For Player Interaction
    void OnMouseOver() {
        if(ControlBinds.GetButtonDown("Interact")) {
            onClick();
        }
    }
}
