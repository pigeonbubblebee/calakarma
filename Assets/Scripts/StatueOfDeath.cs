using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueOfDeath : MonoBehaviour
{
    public Animator animator;

    void Awake()
    {
        if(NodeManager.currentEntranceName.Equals("respawn")) {
            animator.SetTrigger("Respawn");
        }
    }
}
