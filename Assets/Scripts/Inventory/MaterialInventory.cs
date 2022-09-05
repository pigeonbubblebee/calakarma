using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialInventory : Inventory
{
    [Header("Material Inventory References")]
    [SerializeField]
    private TextMeshProUGUI selectedItemPrice;

    // Overrides The Base Method To Incorporate Price
    public override void showSelectedItem(Item item) {
        base.showSelectedItem(item);
        if(item!=null)
            selectedItemPrice.text = getPrice(((MaterialData)(item.getItemData())).sellPrice);
        else
            selectedItemPrice.text = "";
    }

    // Creates A String Made For Descriptions Based On The Price
    public string getPrice(int price) {
        int gold = 0;
        int silver = 0;
        int copper = 0;

        gold = Mathf.FloorToInt(price/100);
        price = price%100;

        silver = Mathf.FloorToInt(price/10);
        price = price%10;

        copper = price;

        return gold + " " + LocalizationSystem.getLocalizedValue("gold:word") + "\n"
        + silver + " " + LocalizationSystem.getLocalizedValue("silver:word") + "\n"
        + copper + " " + LocalizationSystem.getLocalizedValue("copper:word") + "\n";
    }
}
