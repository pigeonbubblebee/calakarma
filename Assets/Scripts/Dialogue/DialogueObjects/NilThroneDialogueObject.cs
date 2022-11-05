using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NilThroneDialogueObject : DialogueNPC
{
    int talkToPlayerCount = 0;

    private NodeDataObject nodeDataObject;

    void Awake()
    {
        nodeDataObject = this.gameObject.GetComponent<NodeDataObject>();
        if(nodeDataObject.getData()==null||nodeDataObject.getData().Equals("")) {
            nodeDataObject.setData(talkToPlayerCount.ToString());
        } else {
            this.talkToPlayerCount = Int32.Parse(nodeDataObject.getData());
        }
    }

    // Dialogue Triggered
    public override void onDialogue(Player p)
    {
        base.onDialogue(p);

        this.talkToPlayerCount = Int32.Parse(nodeDataObject.getData());

        if(UIManager.Instance.uiOpen()) { // Terminates if UI is Open
            return;
        }

        if (p != null)
        {
            if (!UIManager.Instance.uiOpen(UIManager.DialogueBox))
            {
                if (talkToPlayerCount == 0)
                {
                    this.triggerDialogue(0); // Triggers initial dialogue
                    talkToPlayerCount ++;
                }
            }
        }

        nodeDataObject.setData(talkToPlayerCount.ToString());
    }
}
