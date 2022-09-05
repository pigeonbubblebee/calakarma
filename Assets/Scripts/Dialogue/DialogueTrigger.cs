using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Trigger Content")]
    public Dialogue dialogue;

    // Starts Dialogue
    public void triggerDialogue() {
        DialogueManager.Instance.startDialogue(dialogue);
    }
}
