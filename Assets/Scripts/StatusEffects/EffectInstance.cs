using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInstance
{
    public StatusEffect statusEffect;
    public Enemy e;

    float secondTimestamp;

    public float length;
    float inflictedTimestamp;

    public bool effectOver;

    public EffectInstance(StatusEffect statusEffect, Enemy e, float length) {
        this.statusEffect = statusEffect;
        this.e = e;
        this.length = length;
        if(Player.Instance!=null) {
            length = (length*(1+((float)Player.Instance.playerStats.getStatBonus("debufflengthpercent")/100f)));
        }
        this.inflictedTimestamp = Time.time;
    }

    public virtual void updateEffect() {
        statusEffect.updateEffect(e);
        
        if(Time.time > inflictedTimestamp + length) {
            effectOver = true;
        }
    }
}
