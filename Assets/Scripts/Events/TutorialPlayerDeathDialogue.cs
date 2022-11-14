using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerDeathDialogue : MonoBehaviour
{
    bool talked = false;
    private NodeDataObject nodeDataObject;

    public Dialogue dialogue;

    void Awake()
    {
        nodeDataObject = this.gameObject.GetComponent<NodeDataObject>();
        if(nodeDataObject.getData()==null||nodeDataObject.getData().Equals("")) {
            nodeDataObject.setData(talked.ToString());
        } else {
            this.talked = Boolean.Parse(nodeDataObject.getData());
        }

        if(!talked) {
            DialogueManager.Instance.startDialogue(dialogue);

            talked = true;
        }

        nodeDataObject.setData(talked.ToString());
    }
}
