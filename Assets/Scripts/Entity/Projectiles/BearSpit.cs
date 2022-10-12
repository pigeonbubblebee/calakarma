using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpit : ArrowEnemy
{
    public BloodCurse bloodCurse;
    public float bloodCurseLength;
    public int bloodCurseDamage;

    public override void onHit(GameObject player) {
        base.onHit(player);

        player.GetComponent<Player>().playerStats.addStatus(new TickingEffectInstance(bloodCurse, Player.Instance, bloodCurseLength, bloodCurse.damageRate, 2));
    }
}
