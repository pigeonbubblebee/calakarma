
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBorder : MonoBehaviour
{
    [SerializeField]
    private bool left;

    [SerializeField]
    string entranceName = "left";

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Player>()!=null) {
            load();
        }
    }

    void load() {
        if(left) { NodeManager.Instance.loadLeftScene(); }
        else { NodeManager.Instance.loadRightScene(); }
        NodeManager.currentEntranceName = entranceName;
    }
}
