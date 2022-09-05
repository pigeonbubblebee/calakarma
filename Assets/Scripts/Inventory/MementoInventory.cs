using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MementoInventory : Inventory
{
    [Header("Memento Inventory References")]
    [SerializeField]
    private TextMeshProUGUI emotionBasic;
    [SerializeField]
    private TextMeshProUGUI emotionMoving;
    [SerializeField]
    private TextMeshProUGUI emotionPrimal;
    [SerializeField]
    private TextMeshProUGUI emotionFinal;

    [SerializeField]
    private GameObject equipButton;

    public Transform mementoSlotsParent;

    int currentMementoSlotSelected = -1;

    Item selectedItem;

    public GameObject[] mementoHighlights;

    // Overrides The Base Method To Incorporate Stat Bonuses
    public override void showSelectedItem(Item item) {
        selectedItem = item;
        base.showSelectedItem(item);
        if(item!=null) {
            equipButton.SetActive(true);

            emotionBasic.text = getEmotionBasic(((MementoData)(item.getItemData())).emotionBasic);
            emotionMoving.text = getEmotionMoving(((MementoData)(item.getItemData())).emotionMoving);
            emotionPrimal.text = getEmotionPrimal(((MementoData)(item.getItemData())).emotionPrimal);
            emotionFinal.text = getEmotionFinal(((MementoData)(item.getItemData())).emotionFinal);

            int emotionLevel = Player.Instance.playerMemento.getEmotionLevel(item.getItemData().id);

            // emotionLevel = 4;

            string description = this.selectedItemDescription.text+"\n";

            description += LocalizationSystem.getLocalizedValue(item.getItemData().id + "_" + emotionLevel +":mementobonus") + "\n";

            Dictionary<string, int> statBonuses = ((MementoData)(item.getItemData())).updateStatBonuses(Player.Instance, emotionLevel); // Change Emotion Level

            foreach(var(Key, Value) in statBonuses) {
                description += "+" + Value + LocalizationSystem.getLocalizedValue(Key+":statbonus") + "\n";
            }

            string[] otherAttributes = ((MementoData)(item.getItemData())).getOtherAttributes(emotionLevel);

                    for(int i = 0; i < otherAttributes.Length; i++) { // Adds Custom Attributes
                        string currentAttribute = otherAttributes[i];
                        string[] currentAttributeSplit = currentAttribute.Split(";");
                        description+=LocalizationSystem.getLocalizedValue(currentAttributeSplit[0]+":statname") + ": " 
                        + currentAttributeSplit[1] + "\n";
                    }


            selectedItemDescription.text = description;
        }
        else {
            emotionBasic.text = "";
            emotionMoving.text = "";
            emotionPrimal.text = "";
            emotionFinal.text = "";
            equipButton.SetActive(false);
        }
    }

    public override void updateInv() {
        int count = 0;
        foreach(Transform slot in mementoSlotsParent) {
            Image image = null;
            foreach(Transform child in slot) { // Gets Image From Slot
                if(child.GetComponent<Image>()!=null&&image==null)
                    image = child.GetComponent<Image>();
            }

            if(Player.Instance.playerMemento.getEquippedMementos()[count]!=null) { // Render Item
                image.sprite = Player.Instance.playerMemento.getEquippedMementos()[count].getItemData().itemImage;
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

        foreach(GameObject o in mementoHighlights) {
            o.SetActive(false);
        }

        if(currentMementoSlotSelected!=-1)
            mementoHighlights[currentMementoSlotSelected].SetActive(true);
    }

    // Creates A String Made For Descriptions Based On Emotion
    public string getEmotionBasic(MementoData.EmotionBasic emotionBasic) {
        switch(emotionBasic) {
            case MementoData.EmotionBasic.Sadness:
                return LocalizationSystem.getLocalizedValue("sadness:emotion");
            case MementoData.EmotionBasic.Happiness:
                return LocalizationSystem.getLocalizedValue("happiness:emotion");
            case MementoData.EmotionBasic.Anger:
                return LocalizationSystem.getLocalizedValue("anger:emotion");
        }

        return "";
    }
    public string getEmotionMoving(MementoData.EmotionMoving emotionMoving) {
        switch(emotionMoving) {
            case MementoData.EmotionMoving.Regret:
                return LocalizationSystem.getLocalizedValue("regret:emotion");
            case MementoData.EmotionMoving.Sorrow:
                return LocalizationSystem.getLocalizedValue("sorrow:emotion");
            case MementoData.EmotionMoving.Grief:
                return LocalizationSystem.getLocalizedValue("grief:emotion");
            case MementoData.EmotionMoving.Gratitude:
                return LocalizationSystem.getLocalizedValue("gratitude:emotion");
            case MementoData.EmotionMoving.Celebration:
                return LocalizationSystem.getLocalizedValue("celebration:emotion");
            case MementoData.EmotionMoving.Excitement:
                return LocalizationSystem.getLocalizedValue("excitement:emotion");
            case MementoData.EmotionMoving.Lament:
                return LocalizationSystem.getLocalizedValue("lament:emotion");
            case MementoData.EmotionMoving.Revolt:
                return LocalizationSystem.getLocalizedValue("revolt:emotion");
            case MementoData.EmotionMoving.Belligerence:
                return LocalizationSystem.getLocalizedValue("belligerence:emotion");
        }

        return "";
    }
    public string getEmotionPrimal(MementoData.EmotionPrimal emotionPrimal) {
        switch(emotionPrimal) {
            case MementoData.EmotionPrimal.Denial:
                return LocalizationSystem.getLocalizedValue("denial:emotion");
            case MementoData.EmotionPrimal.Isolation:
                return LocalizationSystem.getLocalizedValue("isolation:emotion");
            case MementoData.EmotionPrimal.Anxiety:
                return LocalizationSystem.getLocalizedValue("anxiety:emotion");
            case MementoData.EmotionPrimal.Depression:
                return LocalizationSystem.getLocalizedValue("depression:emotion");
            case MementoData.EmotionPrimal.Cynicality:
                return LocalizationSystem.getLocalizedValue("cynicality:emotion");
            case MementoData.EmotionPrimal.Hospitality:
                return LocalizationSystem.getLocalizedValue("hospitality:emotion");
            case MementoData.EmotionPrimal.Lightheartedness:
                return LocalizationSystem.getLocalizedValue("lightheartedness:emotion");
            case MementoData.EmotionPrimal.Festivity:
                return LocalizationSystem.getLocalizedValue("festivity:emotion");
            case MementoData.EmotionPrimal.Zeal:
                return LocalizationSystem.getLocalizedValue("zeal:emotion");
            case MementoData.EmotionPrimal.Inspiration:
                return LocalizationSystem.getLocalizedValue("inspiration:emotion");
            case MementoData.EmotionPrimal.Hatred:
                return LocalizationSystem.getLocalizedValue("hatred:emotion");
            case MementoData.EmotionPrimal.Purpose:
                return LocalizationSystem.getLocalizedValue("purpose:emotion");
            case MementoData.EmotionPrimal.Rebellion:
                return LocalizationSystem.getLocalizedValue("rebellion:emotion");
            case MementoData.EmotionPrimal.Implacability:
                return LocalizationSystem.getLocalizedValue("implacability:emotion");
            case MementoData.EmotionPrimal.Argument:
                return LocalizationSystem.getLocalizedValue("argument:emotion");
        }

        return "";
    }
    public string getEmotionFinal(MementoData.EmotionFinal emotionFinal) {
        switch(emotionFinal) {
            case MementoData.EmotionFinal.Sage:
                return LocalizationSystem.getLocalizedValue("sage:emotion");
            case MementoData.EmotionFinal.Tranquil:
                return LocalizationSystem.getLocalizedValue("tranquil:emotion");
            case MementoData.EmotionFinal.Control:
                return LocalizationSystem.getLocalizedValue("control:emotion");
        }

        return "";
    }

    public void setCurrentMementoSlotSelected(int i) {
        currentMementoSlotSelected = i;
    }

    public void equip() {
        if(selectedItem!=null && currentMementoSlotSelected != -1) {
            bool equipped = false;
            int equippedIndex = -1;
            int i = 0;
            foreach(Item x in Player.Instance.playerMemento.getEquippedMementos()) {
                if(x!=null) {
                    if(x.getItemData().id.Equals(selectedItem.getItemData().id)) {
                        equipped = true;
                        equippedIndex = i;
                    }
                }
                i++;
            }

            if(equipped && equippedIndex == currentMementoSlotSelected) {
                Player.Instance.playerMemento.setEquippedMemento(null, equippedIndex);
                return;
            }

            if(!equipped) {
                Player.Instance.playerMemento.setEquippedMemento(selectedItem, currentMementoSlotSelected);
            } else {
                Player.Instance.playerMemento.setEquippedMemento(Player.Instance.playerMemento.getEquippedMementos()[currentMementoSlotSelected], equippedIndex);
                Player.Instance.playerMemento.setEquippedMemento(selectedItem, currentMementoSlotSelected);
            }
        }
    }
}