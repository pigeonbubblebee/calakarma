using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChainDialogue", menuName = "Calakarma/DialogueEvent")]
public class ChainDialogueEvent : DialogueEvent
{
    public Dialogue dialogue;

    public override void onEvent() {
        DialogueManager.Instance.startDialogue(dialogue);
    }
}
