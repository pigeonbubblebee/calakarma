using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header("NPC Content")]
    public Dialogue[] dialogues = new Dialogue[0];

    // Starts Dialogue Given An Index
    public void triggerDialogue(int dialogue) {
        DialogueManager.Instance.startDialogue(dialogues[dialogue]);
    }

    // Overrode Method That Triggers When This Object is Clicked
    // Used To Create Dynamic Dialogue Tree Based On External Factors (Ex: Has This Object Been Talked To Yet)
    // Most Likely Uses Trigger Dialogue In Overrode Versions
    public virtual void onDialogue(Player p) {

    }

    // Changes Mouse Cursor When Mouse Goes Over
    void OnMouseEnter()
    {
        if(UIManager.Instance.uiOpen()) { // Stops Changing If UI is Open
            return;
        }
        UIManager.Instance.changeMouse(UIManager.Dialogue_Mouse);
    }

    // Changes Mouse Back When Mouse Leaves
    void OnMouseExit()
    {
        UIManager.Instance.changeMouse(UIManager.Default_Mouse);
    }

    // Checks For Player Interaction
    void OnMouseOver() {
        if(ControlBinds.GetButtonDown("Interact")) {
            Player p = Player.Instance;
            onDialogue(p);
        }
    }
}
