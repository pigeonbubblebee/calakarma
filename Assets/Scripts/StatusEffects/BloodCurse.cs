using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/Blood Curse")]
public class BloodCurse : StatusEffect
{
    public int damage;
    public float damageRate;

    public int damageReduction;
    public int defenseReduction;

    public override void tick(Enemy e, float potency) {
        damageEnemy(e, potency);
    }

    void damageEnemy(Enemy e, float potency) {
        e.takeDamage((int)(damage*(1+potency)));
    }

    public override Dictionary<string, int> statEffects() {
        Dictionary<string, int> result = new Dictionary<string, int>();
        result.Add("damagepercent", -damageReduction);
        result.Add("defensepercent", -defenseReduction);
        return result;
    }
}
