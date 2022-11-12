using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GaryDialogueObject : DialogueNPC
{
    int talkToPlayerCount = 0;

    public GameObject gifts;
    private NodeDataObject nodeDataObject;

    void Awake()
    {
        nodeDataObject = this.gameObject.GetComponent<NodeDataObject>();
        if(nodeDataObject.getData()==null||nodeDataObject.getData().Equals("")) {
            nodeDataObject.setData(talkToPlayerCount.ToString());
        } else {
            this.talkToPlayerCount = Int32.Parse(nodeDataObject.getData());
        }

        if(talkToPlayerCount == 2) {
            gifts.SetActive(true);
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
            if (!(p.playerItemManager.hasItemAmount("stick", 5) && p.playerItemManager.hasItemAmount("timber", 5)))
            {
                if (!UIManager.Instance.uiOpen(UIManager.DialogueBox))
                {
                    if (talkToPlayerCount == 0)
                    {
                        this.triggerDialogue(0); // Triggers initial dialogue
                        talkToPlayerCount ++;
                    }
                    else if(talkToPlayerCount == 1)
                    {
                        this.triggerDialogue(1); // Triggers "Go Gather More" dialogue
                    }
                }
            }
            else
            {
                if (!UIManager.Instance.uiOpen(UIManager.DialogueBox) && talkToPlayerCount < 2)
                {
                    talkToPlayerCount = 2; // Completed all possible dialogues

                    this.triggerDialogue(2); // Triggers "Gift" dialogue

                    gifts.SetActive(true);

                    p.playerItemManager.removeFromInventory(new ItemStack(new Item(ItemRegistry.getItem("stick")), 5));
                    p.playerItemManager.removeFromInventory(new ItemStack(new Item(ItemRegistry.getItem("timber")), 5));
                }
            }
        }

        nodeDataObject.setData(talkToPlayerCount.ToString());
    }
}
