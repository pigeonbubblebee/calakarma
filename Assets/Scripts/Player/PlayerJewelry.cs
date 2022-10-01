using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJewelry : MonoBehaviour
{
    private Item[] equippedJewelry = new Item[3];
    public readonly int jewelrySlots = 3;

    public Item[] getEquippedJewelry() {
        return equippedJewelry;
    }

    public void setEquippedJewelry(Item i, int index) {
        equippedJewelry[index] = i;

        Player.Instance.playerStats.setJewelryStatBonuses(getBonuses());
    }

    public void setEquippedJewelry(Item[] x) {
        equippedJewelry = x;

        Player.Instance.playerStats.setJewelryStatBonuses(getBonuses());
    }

    public Dictionary<string, int> getBonuses() {
        Dictionary<string, int> result = new Dictionary<string, int>();

        foreach(Item i in equippedJewelry) {
            if(i != null) {
                Dictionary<string, int> x = ((JewelryData) (i.getItemData())).updateStatBonuses(Player.Instance);

                foreach(var(id, lvl) in x) {
                    try
                    {
                        result.Add(id, lvl);
                    }
                    catch (ArgumentException)
                    {
                        result[id] += lvl;
                    }
                }
            }
        }

        return result;
    }
}
