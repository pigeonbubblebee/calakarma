using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    [Header("Player Item Manager References")]
    public Inventory[] inventories;

    // Adds An Item To Player's Inventory
    public ItemStack addToInventory(ItemStack item) {
        Player.Instance.playerMemento.setEmotionLevel("wild_spirit", 4); // Temporary

        foreach(Inventory inventory in inventories) {
            if(inventory.itemType==item.getItem().getItemData().itemType) {
                return inventory.addItem(item);
            }
        }

        return null;
    }

    // Removes An Item From The Player's Inventory
    public void removeFromInventory(ItemStack item) {
        foreach(Inventory inventory in inventories) {
            if(inventory.itemType==item.getItem().getItemData().itemType) {
                inventory.removeItem(item);
            }
        }
    }

    // Checks If Player Has A Certain Amount Of An Item
    public bool hasItemAmount(string id, int amount) {
        foreach(Inventory inventory in inventories) {
            foreach(ItemStack i in inventory.getStacks()) {
                if(i.getItem().getItemData().id.Equals(id)&&i.getAmount()>=amount) {
                    return true;
                }
            }
        }

        return false;
    }
}