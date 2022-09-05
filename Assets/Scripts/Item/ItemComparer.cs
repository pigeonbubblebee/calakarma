using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemComparer : IComparer<Item>, IComparer<ItemStack>
{
    // Compares Items
    public int Compare(Item a, Item b) {
        return a.CompareTo(b);
    }

    // Compares ItemStacks
    public int Compare(ItemStack a, ItemStack b) {
        return a.CompareTo(b);
    }
}
