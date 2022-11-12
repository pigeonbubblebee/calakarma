using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathDialogue", menuName = "Calakarma/DeathDialogue")]
public class DeathDialogueEvent : DialogueEvent
{
    public override void onEvent() {
        Player.Instance.playerStats.takeDamage(1000);
    }
}
