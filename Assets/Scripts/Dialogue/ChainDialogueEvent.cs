using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDialogueEvent : DialogueEvent
{
    public Dialogue dialogue;

    public override void onEvent() {
        DialogueManager.Instance.startDialogue(dialogue);
    }
}
