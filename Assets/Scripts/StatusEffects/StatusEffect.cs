using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public bool debuff;

    public string id;

    public virtual void updateEffect(Enemy e) {

    }

    public virtual void tick(Enemy e, float potency) {

    }

    public virtual void updateEffectPlayer(Player p) {

    }

    public virtual void tickPlayer(Player p, float potency) {

    }

    public virtual Dictionary<string, int> statEffects() {
        return new Dictionary<string, int>();
    }
}
