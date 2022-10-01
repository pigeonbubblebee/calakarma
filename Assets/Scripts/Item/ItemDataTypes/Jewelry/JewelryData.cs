using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MementoData", menuName = "Calakarma/MementoItem", order = 5)]
public class JewelryData : ItemData
{
    public string[] otherAttributes;

    public SlotType slotType;

    public enum SlotType {
        Earrings,
        Bracelets,
        Ankles
    }

    public virtual Dictionary<string, int> updateStatBonuses(Player player) {
        resetStats();
        Dictionary<string, int> statBonuses = new Dictionary<string, int>();

        if(this.slotType == SlotType.Earrings) {
            Dictionary<string, int> setBonus = getSetBonus(player);
            foreach(string key in setBonus.Keys) {
                addToStat(key, setBonus[key], statBonuses);
            }
        }

        return statBonuses;
    }

    public virtual Dictionary<string, int> getSetBonus(Player player) {
        Dictionary<string, int> statBonuses = new Dictionary<string, int>();
        return statBonuses;
    }

    public void addToStat(string name, int amount, Dictionary<string, int> statBonuses) {
        try
        {
            statBonuses.Add(name, amount);
        }
        catch (ArgumentException)
        {
            statBonuses[name] = statBonuses[name]+amount;
        }
    }

    public void setStat(string name, int amount, Dictionary<string, int> statBonuses) {
        try
        {
            statBonuses.Add(name, amount);
        }
        catch (ArgumentException)
        {
            statBonuses[name] = amount;
        }
    }

    private void resetStats() {
        Dictionary<string, int> statBonuses = new Dictionary<string, int>();
    }
}
