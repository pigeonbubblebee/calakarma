using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Delegate Class To Handle Animations

    [Header("Player Animations References")]
    public Animator animator;

    // Delegate To Set Bool
    public void setBool(string boolName, bool tf) {
        animator.SetBool(boolName, tf);
    }

    // Delegate To Set Int
    public void setInt(string intName, int val) {
        animator.SetInteger(intName, val);
    }

     // Delegate To Set Trigger
    public void setTrigger(string trigName) {
        animator.SetTrigger(trigName);
    }
}
