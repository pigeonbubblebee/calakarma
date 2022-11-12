using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : Inventory
{
    [Header("Weapon Inventory References")]
    [SerializeField]
    private GameObject equipButton;

    Item selectedItem; // Storage For The Currently Selected Item

    // Overrides The Base Method To Incorporate Weapon Stats
    public override void showSelectedItem(Item item) {
        selectedItem = item;
        base.showSelectedItem(item);
        if(item!=null) {
            equipButton.SetActive(true);
            string description = this.selectedItemDescription.text+"\n"+
            LocalizationSystem.getLocalizedValue("attack:statname") + ": "+((WeaponData)(item.getItemData())).damage + "\n" +
            LocalizationSystem.getLocalizedValue("attackspd:statname") +": "+ (((WeaponData)(item.getItemData())).speed+((WeaponData)(item.getItemData())).chargeSpeed) + "\n";

            if(((WeaponData)(item.getItemData())).weaponType == WeaponData.WeaponType.Sword) { // Adds Charge Speed And Charge Attack
                //description+=LocalizationSystem.getLocalizedValue("chargespd:statname") + ": " +(((SwordData)(item.getItemData())).charge100PercentSpeed) + "\n";
                description+=LocalizationSystem.getLocalizedValue("comboatk:statname") + ": " +(((SwordData)(item.getItemData())).comboDamage) + "\n";
            } else if(((WeaponData)(item.getItemData())).weaponType == WeaponData.WeaponType.Bow) { // Adds Range
                description+=LocalizationSystem.getLocalizedValue("projspd:statname") + ": " +(((BowData)(item.getItemData())).projectileSpeed) + "\n";
                description+=LocalizationSystem.getLocalizedValue("stlthatk:statname") + ": " +(((BowData)(item.getItemData())).stealthDamage) + "\n";
            } else if(((WeaponData)(item.getItemData())).weaponType == WeaponData.WeaponType.Staff) { // Adds Overload Damage
                description+=LocalizationSystem.getLocalizedValue("ovlddmg:statname") + ": " +(((StaffData)(item.getItemData())).overloadDamage) + "\n";
            }

            for(int i = 0; i < ((WeaponData)(item.getItemData())).otherAttributes.Length; i++) { // Adds Custom Attributes
                string currentAttribute = ((WeaponData)(item.getItemData())).otherAttributes[i];
                string[] currentAttributeSplit = currentAttribute.Split(";");
                description+=LocalizationSystem.getLocalizedValue(currentAttributeSplit[0]+":statname") + ": " 
                + currentAttributeSplit[1] + "\n";
            }
            
            selectedItemDescription.text = description; // Sets The Description
        }
        else { // No Item. Hides Equip Button
            equipButton.SetActive(false);
        }
    }
    
    // Method Called By Buttons. Equips The Selected Weapon
    public void equip() {
        Player.Instance.playerCombat.setEquipped(selectedItem);
    }
}
