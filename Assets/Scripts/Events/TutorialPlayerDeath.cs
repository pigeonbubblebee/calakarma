using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerDeath : MonoBehaviour
{
    public Enemy voidBall;

    public Dialogue dialogue;

    bool triggered = false;

    void Update()
    {
        if(Player.Instance.playerStats.health <= 10 && !triggered) {
            voidBall.enabled = false;
            voidBall.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            DialogueManager.Instance.startDialogue(dialogue);
            triggered = true;
        }

        if(voidBall.getHealth() <= 200) {
            voidBall.unconditionalHealthChange(800);
        }
    }
}
