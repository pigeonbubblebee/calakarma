using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WildSpiritData", menuName = "Memento/WildSpirit", order = 0)]
public class WildSpiritData : MementoData
{
    public Bleeding bleeding;
    public Ruptured ruptured;
    public BloodCurse bloodCurse;

    public float bleedingLength;
    public float rupturedLength;
    public float bloodCurseLength;

    public override Dictionary<string, int> updateStatBonuses(Player player, int emotionLevel) {
        Dictionary<string, int> statBonuses = base.updateStatBonuses(player, emotionLevel);

        setStat("debufflengthpercent", 15, statBonuses);
        if(emotionLevel>1) {
            setStat("debufflengthpercent", 25, statBonuses);
        }
        if(emotionLevel>3) {
            setStat("debufflengthpercent", 35, statBonuses);
        }

        return statBonuses;
    }

    public override void onProjectileCollision(Collision2D collision, int emotionLevel) {
        
        // BLEED

        if(emotionLevel > 0) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bleeding, e, bleedingLength, bleeding.damageRate, 0));
        }

        if(emotionLevel > 2) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new EffectInstance(ruptured, e, rupturedLength));
        }

        if(emotionLevel > 3) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bloodCurse, e, bloodCurseLength, bloodCurse.damageRate, 2));
        }
    }

    public override void onMagicProjectileCollision(Collision2D collision, int emotionLevel) {
        // BLEED

        if(emotionLevel > 0) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bleeding, e, bleedingLength, bleeding.damageRate, 0));
        }

        if(emotionLevel > 2) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new EffectInstance(ruptured, e, rupturedLength));
        }

        if(emotionLevel > 3) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bloodCurse, e, bloodCurseLength, bloodCurse.damageRate, 2));
        }
    }

    public override void onMeleeCollision(Collider2D collision, int emotionLevel) {
        // BLEED

        if(emotionLevel > 0) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bleeding, e, bleedingLength, bleeding.damageRate, 0));
        }

        if(emotionLevel > 2) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new EffectInstance(ruptured, e, rupturedLength));
        }

        if(emotionLevel > 3) {
            Enemy e = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

            e.addStatus(new TickingEffectInstance(bloodCurse, e, bloodCurseLength, bloodCurse.damageRate, 2));
        }
    }
}
