using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    [Header("Item Entity References")]
    public ItemData itemData;

    private Item item;

    // Initializes Item
    void Awake() {
        item = new Item(itemData);
    }

    // Constructor
    public ItemEntity(Item i) {
        item = i;
    }

    // Changes Item
    public void setItem(Item set) {
        item = set;
    }

    // Getter Method For Item
    public Item getItem() {
        return item;
    }
}
