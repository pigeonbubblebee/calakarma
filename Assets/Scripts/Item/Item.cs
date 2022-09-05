using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : IComparable<Item>
{
    ItemData itemData;
    
    // Constructor
    public Item(ItemData itemData) {
        this.itemData = itemData;
    }

    // First Compares With Tier, Then Name
    public int CompareTo(Item other) {
        if(other.getItemData().itemTier<this.itemData.itemTier) {
            return 1;
        } else if(other.getItemData().itemTier>this.itemData.itemTier) {
            return -1;
        } else {
            return this.getItemData().id.CompareTo(other.itemData.id);
        }
    }

    // Getter Method For This Item's ItemData
    public ItemData getItemData() {
        return itemData;
    }
}
