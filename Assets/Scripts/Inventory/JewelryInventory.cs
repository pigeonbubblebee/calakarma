using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JewelryInventory : Inventory
{
    [SerializeField]
    private GameObject equipButton;

    public Transform jewelrySlotsParent;

    int currentJewelrySlotSelected = -1;

    Item selectedItem;

    public GameObject[] jewelryHighlights;


    // Overrides The Base Method To Incorporate Stat Bonuses
    public override void showSelectedItem(Item item) {
        selectedItem = item;
        base.showSelectedItem(item);
        if(item!=null) {
            equipButton.SetActive(true);
            string description = this.selectedItemDescription.text+"\n";

            Dictionary<string, int> statBonuses = ((JewelryData)(item.getItemData())).updateStatBonuses(Player.Instance);

            foreach(var(Key, Value) in statBonuses) {
                description += "+" + Value + LocalizationSystem.getLocalizedValue(Key+":statbonus") + "\n";
            }

            string[] otherAttributes = ((JewelryData)(item.getItemData())).otherAttributes;

                    for(int i = 0; i < otherAttributes.Length; i++) { // Adds Custom Attributes
                        string currentAttribute = otherAttributes[i];
                        string[] currentAttributeSplit = currentAttribute.Split(";");
                        description+=LocalizationSystem.getLocalizedValue(currentAttributeSplit[0]+":statname") + ": " 
                        + currentAttributeSplit[1] + "\n";
                    }


            selectedItemDescription.text = description;
        }
        else {
            equipButton.SetActive(false);
        }
    }

    public override void updateInv() {
        int count = 0;
        foreach(Transform slot in jewelrySlotsParent) {
            Image image = null;
            foreach(Transform child in slot) { // Gets Image From Slot
                if(child.GetComponent<Image>()!=null&&image==null)
                    image = child.GetComponent<Image>();
            }

            if(Player.Instance.playerJewelry.getEquippedJewelry()[count]!=null) { // Render Item
                image.sprite = Player.Instance.playerJewelry.getEquippedJewelry()[count].getItemData().itemImage;
                Color c = image.color;
                c.a = 1f;
                image.color = c;
            } else { // No Item. Hides Image
                image.sprite = null;
                Color c = image.color;
                c.a = 0f;
                image.color = c;
            }
            count++;
        }

        foreach(GameObject o in jewelryHighlights) {
            o.SetActive(false);
        }

        if(currentJewelrySlotSelected!=-1)
            jewelryHighlights[currentJewelrySlotSelected].SetActive(true);
    }

    public void setCurrentJewelrySlotSelected(int i) {
        currentJewelrySlotSelected = i;
    }

    public void equip() {
        if(selectedItem!=null && currentJewelrySlotSelected != -1) {
            bool equipped = false;
            int equippedIndex = -1;
            int i = 0;
            foreach(Item x in Player.Instance.playerJewelry.getEquippedJewelry()) {
                if(x!=null) {
                    if(x.getItemData().id.Equals(selectedItem.getItemData().id)) {
                        equipped = true;
                        equippedIndex = i;
                    }
                }
                i++;
            }

            if(equipped && equippedIndex == currentJewelrySlotSelected) {
                Player.Instance.playerJewelry.setEquippedJewelry(null, equippedIndex);
                return;
            }

            if(!equipped) {
                Player.Instance.playerJewelry.setEquippedJewelry(selectedItem, currentJewelrySlotSelected);
            } else {
                Player.Instance.playerJewelry.setEquippedJewelry(Player.Instance.playerJewelry.getEquippedJewelry()[currentJewelrySlotSelected], equippedIndex);
                Player.Instance.playerJewelry.setEquippedJewelry(selectedItem, currentJewelrySlotSelected);
            }
        }
    }
}
