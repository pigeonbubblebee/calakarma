using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Calakarma/Item", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Item Settings")]
    public string id;
    public ItemType itemType;
    public int itemTier;
    public int maxStack = 999;
    public Sprite itemImage; // Item In Inv
    public Sprite itemImageInWorld; // Item Entity

    [Serializable]
    // Enum To Determine Which Inventory The Item Goes To
    public enum ItemType {
        Weapon,
        Consumable,
        Memento,
        Currency,
        Material,
        Lore
    }

    // Creates a String Based On This Item's Tier
    public string generateAttribute() {
        if(itemTier == 1) {
            return LocalizationSystem.getLocalizedValue("common:word") + " Item";
        }
        if(itemTier == 2) {
            return LocalizationSystem.getLocalizedValue("uncommon:word") + " Item";
        }
        if(itemTier == 3) {
            return LocalizationSystem.getLocalizedValue("rare:word") + " Item";
        }
        if(itemTier == 4) {
            return LocalizationSystem.getLocalizedValue("legendary:word") + " Item";
        }
        if(itemTier == 5) {
            return LocalizationSystem.getLocalizedValue("mythic:word") + " Item";
        }

        return "";
    }
}

