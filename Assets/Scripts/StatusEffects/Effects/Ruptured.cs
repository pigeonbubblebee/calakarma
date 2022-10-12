using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/Ruptured")]
public class Ruptured : StatusEffect
{
    public int extraDamageTaken;

    public override Dictionary<string, int> statEffects() {
        Dictionary<string, int> result = new Dictionary<string, int>();
        result.Add("damagetakenpercent", extraDamageTaken);
        return result;
    }
}