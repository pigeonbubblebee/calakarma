using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Tymski;
using UnityEngine.SceneManagement;

public class Save
{
    static Dictionary<string, string> nodeDatas = new Dictionary<string, string>();
    static Inventory[] inventories;
    static Item[] mementos;
    static int totalMementoSlotsUnlocked = 3;
    static Item equippedWeapon;
    static Dictionary<string, int> mementoEmotionLevels = new Dictionary<string, int>();

    static int health = 100;
    static int mana = 100;

    static string scene;
    static string respawnScene;

    static Item[] jewelry;

    public static int currentSaveSlot = 1;

    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
                    + "/CalakarmaSave" + currentSaveSlot + ".dat"); 
        SaveData data = new SaveData();

        data.nodeDatas = nodeDatas;
        data.inventories = new InventoryDataSerializable[inventories.Length];
        data.equippedMementoIDs = new string[Player.Instance.playerMemento.getEquippedMementos().Length];
        data.equippedJewelryIDs = new string[3];

        if(scene != null) {
            data.scene = scene;
        }

        data.respawnScene = respawnScene;
        
        for(int i = 0; i < inventories.Length; i++) {
            data.inventories[i] = new InventoryDataSerializable(new string[inventories[i].getStacks().Count], new int[inventories[i].getStacks().Count]);
            //data.inventories[i].stacks = ;
            int index = 0;
            foreach(ItemStack item in inventories[i].getStacks()) {
                (data.inventories[i]).stacks[index] = item.getItem().getItemData().id;
                (data.inventories[i]).stackSizes[index] = item.getAmount();
                index++;
            }
        }

        data.totalMementoSlotsUnlocked = totalMementoSlotsUnlocked;

        for(int i = 0; i < Player.Instance.playerMemento.getEquippedMementos().Length; i++) {
            if(Player.Instance.playerMemento.getEquippedMementos()[i]!=null)
                data.equippedMementoIDs[i] = mementos[i].getItemData().id;
        }

        data.mementoEmotionLevels = mementoEmotionLevels;

        if(equippedWeapon!=null)
            data.equippedWeaponID = equippedWeapon.getItemData().id;

        data.playerHealth = health;
        data.playerMana = mana;

        for(int i = 0; i < Player.Instance.playerJewelry.getEquippedJewelry().Length; i++) {
            if(Player.Instance.playerJewelry.getEquippedJewelry()[i]!=null)
                data.equippedJewelryIDs[i] = jewelry[i].getItemData().id;
        }

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public static void LoadGame()
    {
        GameObject.Find("Player").GetComponent<Player>().setSingleton();
        if (File.Exists(Application.persistentDataPath 
                    + "/CalakarmaSave" + currentSaveSlot + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                    File.Open(Application.persistentDataPath 
                    + "/CalakarmaSave" + currentSaveSlot + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            nodeDatas = data.nodeDatas;
            inventories = new Inventory[data.inventories.Length];
            for(int i = 0; i < data.inventories.Length; i++) {
                
                inventories[i] = Player.Instance.playerItemManager.inventories[i];
                
                SortedSet<ItemStack> stacks = new SortedSet<ItemStack>(new ItemComparer());
                int index = 0;
                foreach(String s in data.inventories[i].stacks) {
                    stacks.Add(new ItemStack(new Item(ItemRegistry.getItem(s)), data.inventories[i].stackSizes[index]));
                    index++;
                }

                inventories[i].setData(stacks);
            }

            totalMementoSlotsUnlocked = data.totalMementoSlotsUnlocked;

            mementos = new Item[totalMementoSlotsUnlocked];

            if(data.scene != null) {
                scene = data.scene;
            }

            respawnScene = data.respawnScene;

            for(int i = 0; i < data.equippedMementoIDs.Length; i++) {
                if(data.equippedMementoIDs[i]!=null) {
                    mementos[i] = new Item(ItemRegistry.getItem(data.equippedMementoIDs[i]));
                }
            }

            mementoEmotionLevels = data.mementoEmotionLevels;

            if(data.equippedWeaponID!=null)
                equippedWeapon = new Item(ItemRegistry.getItem(data.equippedWeaponID));

            health = data.playerHealth;
            mana = data.playerMana;

            for(int i = 0; i < data.equippedJewelryIDs.Length; i++) {
                if(data.equippedJewelryIDs[i]!=null) {
                    jewelry[i] = new Item(ItemRegistry.getItem(data.equippedJewelryIDs[i]));
                }
            }
            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
    }

    public static void LoadScene() {
        if (File.Exists(Application.persistentDataPath 
                    + "/CalakarmaSave" + currentSaveSlot + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                    File.Open(Application.persistentDataPath 
                    + "/CalakarmaSave" + currentSaveSlot + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            scene = data.scene;
            respawnScene = data.respawnScene;
            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
    }

    public static void AutoSave() {
        if(Player.Instance!=null) {
            setInventory(Player.Instance.playerItemManager.inventories);
            setEquippedWeapon(Player.Instance.playerCombat.getEquipped());
            setHealth(Player.Instance.playerStats.health);
            setMana(Player.Instance.playerStats.mana);
            setMementoSlots(Player.Instance.playerMemento.mementoSlots);
            setMementos(Player.Instance.playerMemento.getEquippedMementos());
            setEmotionLevels(Player.Instance.playerMemento.getEmotionLevels());
            setJewelry(Player.Instance.playerJewelry.getEquippedJewelry());
        }
        
        setRespawnScene(Player.respawnScene);
        scene = SceneManager.GetActiveScene().name;

        if(NodeManager.Instance!=null) {
            NodeManager.Instance.updateNodeData();
            addNodeData(NodeManager.Instance.getId(), NodeManager.Instance.getData());
        }

        SaveGame();
    }

    public static void addNodeData(string nodeID, string data) {
        try
        {
            nodeDatas.Add(nodeID, data);
        }
        catch (ArgumentException)
        {
            nodeDatas[nodeID] = data;
        }
    }

    public static string getNodeData(string key) {
        string value = "NULL";
        nodeDatas.TryGetValue(key, out value);
        return value;
    }

    public static void setInventory(Inventory[] inventories) {
        Save.inventories = inventories;
    }

    public static Inventory[] getInventory() {
        return inventories;
    }

    public static void setEquippedWeapon(Item weapon) {
        equippedWeapon = weapon;
    }

    public static Item getEquippedWeapon() {
        return equippedWeapon;
    }

    public static void setHealth(int h) {
        health = h;
    }

    public static int getHealth() {
        return health;
    }

    public static void setMana(int m) {
        mana = m;
    }

    public static int getMana() {
        return mana;
    }

    public static int getMementoSlots() {
        return totalMementoSlotsUnlocked;
    }

    public static void setMementoSlots(int i) {
        totalMementoSlotsUnlocked = i;
    }

    public static void setMementos(Item[] mementos) {
        Save.mementos = mementos;
    }

    public static Item[] getMementos() {
        return mementos;
    }

    public static void addEmotionLevel(string mementoID, int level) {
        try
        {
            mementoEmotionLevels.Add(mementoID, level);
        }
        catch (ArgumentException)
        {
            mementoEmotionLevels[mementoID] = level;
        }
    }

    public static int getEmotionLevel(string key) {
        int value = 0;
        mementoEmotionLevels.TryGetValue(key, out value);
        return value;
    }

    public static Dictionary<string, int> getEmotionLevels() {
        return mementoEmotionLevels;
    }

    public static void setEmotionLevels(Dictionary<string, int> x) {
        mementoEmotionLevels = x;
    }

    public static string getScene() {
        return scene;
    }

    public static void setScene(string scene) {
        Save.scene = scene;
    }

    public static string getRespawnScene() {
        return respawnScene;
    }

    public static void setRespawnScene(string scene) {
        Save.respawnScene = scene;
    }

        public static void setJewelry(Item[] jewelry) {
        Save.jewelry = jewelry;
    }

    public static Item[] getJewelry() {
        return jewelry;
    }
}

[Serializable]
class SaveData {
    public Dictionary<string, string> nodeDatas = new Dictionary<string, string>();
    public InventoryDataSerializable[] inventories;
    public string equippedWeaponID;

    public string[] equippedMementoIDs;
    public int totalMementoSlotsUnlocked;

    public Dictionary<string, int> mementoEmotionLevels = new Dictionary<string, int>();

    public string scene;
    public string respawnScene;

    public int playerHealth;
    public int playerMana;

    public string[] equippedJewelryIDs;
}

[Serializable]
class InventoryDataSerializable {
    public string[] stacks = new string[0];
    public int[] stackSizes = new int[0];

    public InventoryDataSerializable() {

    }

    public InventoryDataSerializable(string[] stacks, int[] stackSizes) {
        this.stacks = stacks;
        this.stackSizes = stackSizes;
    }
}