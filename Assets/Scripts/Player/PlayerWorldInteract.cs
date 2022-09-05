using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInteract : MonoBehaviour
{
    [Header("Item Pickup Settings")]
    public float itemPickupRad = 0.7f;
    public Transform itemPickupLocation;

    // Scans For Items To Pick Up
    void Update()
    {
        Collider2D[] pickupResults = 
        Physics2D.OverlapCircleAll((Vector2)itemPickupLocation.position, itemPickupRad);

        foreach(Collider2D c in pickupResults) {
            GameObject pickupObject = c.gameObject;
            ItemEntity i = pickupObject.GetComponent<ItemEntity>();
            if(i != null) {
                ItemStack iStack = Player.Instance.playerItemManager.addToInventory(new ItemStack(i.getItem(), 1)); // Adds Item To Player Inventory
                if(iStack==null)
                    Destroy(i.gameObject);
            }
        }
    }
}