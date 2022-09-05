
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings & References")]
    public ItemData.ItemType itemType;

    [SerializeField]
    int stacksPerScreen;
    [SerializeField]
    int totalScreens;

    int maxStacks => stacksPerScreen*totalScreens;

    public Transform slotsParent;

    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public Image selectedItemImage;

    public GameObject itemScrollBar;

    [Header("Page Changers")]

    public TextMeshProUGUI prevPageNumb;
    public TextMeshProUGUI nextPageNumb;

    SortedSet<ItemStack> stacks = new SortedSet<ItemStack>(new ItemComparer()); // Actual Storage Of Items. Automatically Sorted
    ItemStack[,] screens; // Grid Of Items For Visual Purposes

    int currentPage;

    // Initializes ItemStack Grid
    void Start() {
        if(screens == null)
            screens = new ItemStack[totalScreens, stacksPerScreen];
    }

    // Adds An Item To Inventory. Returns Excess Items
    public ItemStack addItem(ItemStack item) {
        foreach(ItemStack i in stacks) { // Iterate Through Items Held
            if(i.getItem().getItemData().id.Equals(item.getItem().getItemData().id)) { // Checks If Item Ids Are Equal
                int total = item.getAmount()+i.getAmount();
                if(total<=item.getItem().getItemData().maxStack) { // Fills Slots Without Excess
                    i.setAmount(total);
                    sortScreens();
                    return null;
                } else { // Fills Slots Then Returns Excess
                    int amountAdded = i.getItem().getItemData().maxStack-i.getAmount();
                    i.setAmount(i.getItem().getItemData().maxStack);
                    sortScreens();
                    return new ItemStack(item.getItem(), total-amountAdded);
                }
            }
        }
        if(stacks.Count<maxStacks) { // Creates New Stack (Item Not Currently In Stacks)
            stacks.Add(item);
            sortScreens();
        } else {
            return item; // Full Inventory
        }

        return null;
    }

    // Removes An Item From Inventory
    public void removeItem(ItemStack item) {
        foreach(ItemStack i in stacks) { // Iterate Through Items Held
            if(i.getItem().getItemData().id.Equals(item.getItem().getItemData().id)) { // Checks If Item Ids Are Equal
                i.setAmount(Mathf.Max(i.getAmount()-item.getAmount(), 0));
                if(i.getAmount() == 0) {
                    stacks.Remove(i);
                }
                sortScreens();
                return;
            }
        }
    }

    // Updates ItemStack Grid To Match Sorted Set
    public void sortScreens() {
        int count = 0;
        //if(screens == null) {
            screens = new ItemStack[totalScreens, stacksPerScreen]; // Re-initializes Grid
        //}
        foreach(ItemStack item in stacks) {
            if(item!=null) {
                screens[(int)(Mathf.Floor(count/stacksPerScreen)), count%stacksPerScreen] = new ItemStack(item.getItem(), item.getAmount());
            } else {
                screens[(int)(Mathf.Floor(count/stacksPerScreen)), count%stacksPerScreen] = null;
            }
            count++;
        }
    }

    // Renders Slots And Updates Page Numbers
    void Update()
    {
        renderSlots();
        updateInv();

        if(nextPageNumb!=null&&prevPageNumb!=null) {
            if(currentPage<totalScreens-1) {
                nextPageNumb.text = (currentPage+2).ToString();
            } else {
                nextPageNumb.text = "1";
            }

            if(currentPage>0) {
                prevPageNumb.text = currentPage.ToString();
            } else {
                prevPageNumb.text = totalScreens.ToString();
            }
        }
    }

    public virtual void updateInv() {

    }

    // Flips To Next Page
    public void nextPage() {
        if(currentPage<totalScreens-1) {
            currentPage++;
        } else {
            currentPage = 0;
        }
    }

    // Flips To Previous Page
    public void prevPage() {
        if(currentPage>0) {
            currentPage--;
        } else {
            currentPage = totalScreens-1;
        }
    }

    // Getter Method For The Current Page
    public int getCurrentPage() {
        return currentPage;
    }

    // Goes Through Slots And Applies Text And Images To It
    void renderSlots() {
        int count = 0;
        foreach(Transform slot in slotsParent) {
            Image image = null;
            TextMeshProUGUI text = null;
            foreach(Transform child in slot) { // Gets Text And Images From Slot
                if(child.GetComponent<Image>()!=null&&image==null)
                    image = child.GetComponent<Image>();
        
                if(child.GetComponent<TextMeshProUGUI>()!=null)
                    text = child.GetComponent<TextMeshProUGUI>();
            }
            if(screens[currentPage, count]!=null) { // Render Item
                image.sprite = screens[currentPage, count].getItem().getItemData().itemImage;
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                text.text = screens[currentPage, count].getAmount().ToString();
            } else { // No Item. Hides Image And Text
                image.sprite = null;
                Color c = image.color;
                c.a = 0f;
                image.color = c;
                text.text = "";
            }
            count++;
        }
    }

    // Getter Method For ItemStack Grid
    public SortedSet<ItemStack> getStacks() {
        return stacks;
    }

    // Starts Item Description Display Process
    public void startSelectedItem(int slotNum) {
        if(screens[currentPage, slotNum]==null) {
            showSelectedItem(null);
            return;
        }
        showSelectedItem(screens[currentPage, slotNum].getItem());
    }

    // Overrode Method That Displays The Current Selected Item
    public virtual void showSelectedItem(Item item) {
        if(item!=null) { // Displays Item
            selectedItemName.text = LocalizationSystem.getLocalizedValue(item.getItemData().id+":item");
            string description = LocalizationSystem.getLocalizedValue(item.getItemData().id+":itemdesc") + "\n\n" + "Attributes: \n" +
                item.getItemData().generateAttribute(); 
            selectedItemDescription.text = description;
            selectedItemImage.sprite = item.getItemData().itemImage;
            Color c = selectedItemImage.color;
            c.a = 1f;
            selectedItemImage.color = c;
            itemScrollBar.SetActive(true);
        } else { // No Item. Hides Text And Images
            selectedItemName.text = LocalizationSystem.getLocalizedValue("no_selected:word");
            selectedItemDescription.text = "";
            selectedItemImage.sprite = null;
            Color c = selectedItemImage.color;
            c.a = 0f;
            selectedItemImage.color = c;
            itemScrollBar.SetActive(false);
        }
    }

    // Directly Sets Data
    public void setData(SortedSet<ItemStack> stacks_) {
        this.stacks = stacks_;
    }
}