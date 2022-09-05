using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMemento : MonoBehaviour
{
    private Item[] equippedMementos = new Item[3];
    public int mementoSlots = 3;

    private Dictionary<string, int> emotionLevels = new Dictionary<string, int>();

    public Item[] getEquippedMementos() {
        return equippedMementos;
    }

    public void setEquippedMemento(Item i, int index) {
        equippedMementos[index] = i;

        Player.Instance.playerStats.statBonuses = getBonuses();
    }

    public void setEquippedMementos(Item[] x) {
        equippedMementos = x;

        Player.Instance.playerStats.statBonuses = getBonuses();
    }

    public int getEmotionLevel(string id) {
        int value = 0;
        emotionLevels.TryGetValue(id, out value);
        return value;
    }

    public void setEmotionLevel(string id, int i) {
        try
        {
            emotionLevels.Add(id, i);
        }
        catch (ArgumentException)
        {
            emotionLevels[id] = i;
        }

        Player.Instance.playerStats.statBonuses = getBonuses();
    }

    public void setEmotionLevels(Dictionary<string, int> x) {
        emotionLevels = x;

        Player.Instance.playerStats.statBonuses = getBonuses();
    }

    public Dictionary<string, int> getEmotionLevels() {
        return emotionLevels;
    }

    public Dictionary<string, int> getBonuses() {
        Dictionary<string, int> result = new Dictionary<string, int>();

        foreach(Item i in equippedMementos) {
            if(i != null) {
                Dictionary<string, int> x = ((MementoData) (i.getItemData())).updateStatBonuses(Player.Instance, getEmotionLevel(i.getItemData().id));
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
