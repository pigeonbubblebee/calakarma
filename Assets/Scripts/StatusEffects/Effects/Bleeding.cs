using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StatusEffect/Bleeding")]
public class Bleeding : StatusEffect
{
    public float damageRate;

    public override void tick(Enemy e, float potency) {
        damageEnemy(e, potency);
    }

    void damageEnemy(Enemy e, float potency) {
        e.takeDamage((int)((1+potency)));
    }

    public override void tickPlayer(Player p, float potency) {
        p.playerStats.takeDamage(((int)((1+potency))));
    }
}
