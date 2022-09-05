
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDataObject : MonoBehaviour
{
    [SerializeField]private string currentData;

    public string id;

    public void setData(string s) {
        currentData=s;
    }

    public string getData() {
        return currentData;
    }
}
