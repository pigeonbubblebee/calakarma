using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack : IComparable<ItemStack>
{
    Item item;
    int amount;

    // Constructor
    public ItemStack(Item item, int amount) {
        this.item = item;
        this.amount = amount;
    }

    // Compares With A Different Item Stack
    public int CompareTo(ItemStack other) {
        return this.item.CompareTo(other.item);
    }

    // Getter Method For Item
    public Item getItem() {
        return item;
    }

    // Getter Method For Amount
    public int getAmount() {
        return amount;
    }

    // Setter Method For Amount
    public void setAmount(int amount) {
        this.amount = amount;
    }
}
