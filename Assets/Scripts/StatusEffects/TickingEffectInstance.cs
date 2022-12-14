using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickingEffectInstance : EffectInstance
{
    float tickTimestamp;
    float tickRate;
    float potency;

    public override void updateEffect() {
        base.updateEffect();

        if(Time.time > tickTimestamp)  {
            tickTimestamp = Time.time+tickRate;
            statusEffect.tick(e, potency);
        }
    }

    public override void updateEffectPlayer() {
        base.updateEffect();

        if(Time.time > tickTimestamp)  {
            tickTimestamp = Time.time+tickRate;
            statusEffect.tickPlayer(p, potency);
        }
    }

    public TickingEffectInstance(StatusEffect statusEffect_, Enemy e_, float length_, float tickRate, float potency) : base(statusEffect_, e_, length_) {
        this.tickRate = tickRate;
        this.potency = potency;
    }

    public TickingEffectInstance(StatusEffect statusEffect_, Player p_, float length_, float tickRate, float potency) : base(statusEffect_, p_, length_) {
        this.tickRate = tickRate;
        this.potency = potency;
    }
}
