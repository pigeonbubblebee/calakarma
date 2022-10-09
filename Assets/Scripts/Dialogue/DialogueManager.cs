using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image image;
    
    // Singleton Pattern
    private static DialogueManager _instance;
    public static DialogueManager Instance { get { return _instance; } }

    private Queue<string> dialogueQueue = new Queue<string>(); // Dialogue Handler

    private DialogueEvent dialogueEvent;
    
    // Implements Singleton Pattern
    void Awake()
    {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Initializes Dialogue
    public void startDialogue(Dialogue dialogue) {
        UIManager.Instance.toggleUIOn(UIManager.DialogueBox);
        nameText.text = LocalizationSystem.getLocalizedValue(dialogue.sourceName+":name"); // Gets Name and Image of the Dialogue Source
        image.sprite = dialogue.sourceImage;
        dialogueQueue.Clear(); // Clears All Current Dialogue
        dialogueEvent = dialogue.dialogueEvent;
        foreach(string x in dialogue.dialogues) { // Adds All Dialogue From Argument To The Queue
            dialogueQueue.Enqueue(x);
        }

        displayNextSentence(); // Starts Dialogue Display Chain
    }

    // Displays The Next Piece of Dialogue
    public void displayNextSentence() {
        if(dialogueQueue.Count == 0) { // Terminates if All Dialogue is Done
            endDialogue();
            if(dialogueEvent!=null) {
                dialogueEvent.onEvent();
            }
            return;
        }

        string dialogue = dialogueQueue.Dequeue(); // Iterates Dialogue Queue
        dialogueText.text = LocalizationSystem.getLocalizedValue(dialogue+":dialogue"); // Displays Current Dialogue
    }

    // Terminates Dialogue
    void endDialogue() {
        UIManager.Instance.toggleUIOff(UIManager.DialogueBox);
    }

    public void Update() {
        if(ControlBinds.GetButtonDown("Dialogue")) {
            displayNextSentence(); // Goes to Next Piece Of Dialogue When Corresponding Key Is Pressed NOTE: Needs to Be Changed To Work With Keybinds
        }
    }
}