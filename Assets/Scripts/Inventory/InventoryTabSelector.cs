using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTabSelector : MonoBehaviour
{
    [Header("Tab Selector Content")]
    public GameObject[] tabs; // Inventories

    public GameObject[] tabBorders; // Inventories

    // Opens The Appropriate Tab. Called By Buttons
    public void openTab(int index) {
        for(int i = 0; i < tabs.Length; i++) {
            tabs[i].SetActive(false);
            tabBorders[i].SetActive(false);
        }

        tabs[index].SetActive(true);
        tabBorders[index].SetActive(true);
    }
}
