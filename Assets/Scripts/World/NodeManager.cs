using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tymski;

public class NodeManager : MonoBehaviour
{
    [Header("Scenes Next To This One")]
    public SceneReference leftScene;
    public SceneReference rightScene;

    // Singleton Pattern
    private static NodeManager _instance;
    public static NodeManager Instance { get { return _instance; } }

    [SerializeField]
    private string id;

    [SerializeField]
    private string nodeData;

    [SerializeField]
    private Transform[] entrancePositions;
    [SerializeField]
    private string[] entranceNames;

    [SerializeField]List<GameObject> destroyables = new List<GameObject>();
    [SerializeField]List<GameObject> objects = new List<GameObject>();

    public static string currentEntranceName = "left";

    public static float saveTimestamp;

    public string getId() {
        return id;
    }

    public string getData() {
        return nodeData;
    }

    void OnApplicationQuit()
    {
        Save.AutoSave();
    }

    void Update()
    {
        if(SettingsSave.autoSave) {
            if(saveTimestamp % 600 == 0) {
                saveTimestamp = Time.time;
                Save.AutoSave();
            }
        }
    }

    // Implements Singleton Pattern And Loads Save Data
    void Awake()
    {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        Save.LoadGame();

        if(Save.getInventory() != null)
            Player.Instance.playerItemManager.inventories = Save.getInventory();
        if(Save.getEquippedWeapon() != null) 
            Player.Instance.playerCombat.setEquipped(Save.getEquippedWeapon());

        Player.Instance.playerStats.health = Save.getHealth();
        Player.Instance.playerStats.mana = Save.getMana();

        Player.Instance.playerMemento.mementoSlots = Save.getMementoSlots();

        if(Save.getMementos()!=null)
            Player.Instance.playerMemento.setEquippedMementos(Save.getMementos());

        if(Save.getJewelry()!=null)
            Player.Instance.playerJewelry.setEquippedJewelry(Save.getJewelry());

        if(Save.getEmotionLevels()!=null)
            Player.Instance.playerMemento.setEmotionLevels(Save.getEmotionLevels());

        Player.respawnScene = Save.getRespawnScene();

        string saveData = Save.getNodeData(id);
        if(saveData!="NULL") {
            nodeData = saveData;

            if(nodeData!=null)
                parseData();
        }

        foreach(Inventory i in Player.Instance.playerItemManager.inventories) {

            
            if(i!=null)
                i.sortScreens();
            
        }

        int x = Array.IndexOf(entranceNames, currentEntranceName);
        Player.Instance.transform.position = entrancePositions[x].position;

        Save.setScene(SceneManager.GetActiveScene().name);

        Player.Instance.playerStats.setMementoStatBonuses(Player.Instance.playerMemento.getBonuses());

    
       
    }

    public void loadLeftScene() {
        Save.AutoSave();
        if(leftScene!=null)
            SceneManager.LoadScene(leftScene);
    }

    public void loadRightScene() {
        Save.AutoSave();
        if(rightScene!=null)
            SceneManager.LoadScene(rightScene);
    }

    // Getter for node data
    public string loadData() {
        return nodeData;
    }

    private void parseData() {
        string[] strSplit = nodeData.Split(";");
        HashSet<string> objectsNames = new HashSet<string>();

        foreach(string s in strSplit) {
            string[] subSplit = s.Split("|");

            if(subSplit.Length!=1) {
                if(subSplit[0].Equals("DATANODEOBJECT")) {
                    string[] iddata = subSplit[1].Split(":");
                    
                    foreach(GameObject g in objects) {
                        
                        if(g.GetComponent<NodeDataObject>().id.Equals(iddata[0])) {
                            g.GetComponent<NodeDataObject>().setData(iddata[1]);
                            break;
                        }
                    }
                } else {
                    objectsNames.Add(subSplit[1]);
                    
                }
            }
        }
        
        foreach(GameObject g in destroyables) {
            bool x = false;
            
            foreach(string s in objectsNames) {

                if(s.Equals(g.name)) {
                    x = true;
                }
            }

            if(!x) {
                Destroy(g);
            }
        }
    }

    private static string compileDestroyableData(List<GameObject> dest_) {
        string result = "";

        foreach(GameObject i in dest_) {
            
            if(i!=null) {
                if(i.GetComponent<Enemy>()!=null) {
                    if(i.GetComponent<Enemy>().dead) continue;
                }
                result+="DATADESTENTITY|";
                result+=i.name;
                result+=";";
            }
        }

        return result;
    }

    private static string compileObjectData(List<GameObject> objects_) {
        string result = "";

        foreach(GameObject o in objects_) {
            result+="DATANODEOBJECT|";
            result+=o.GetComponent<NodeDataObject>().id;
            result+=":";
            result+=o.GetComponent<NodeDataObject>().getData();
            result+=";";
        }

        return result;
    }

    public void updateNodeData() {
        this.nodeData = "";
        nodeData+=NodeManager.compileDestroyableData(destroyables);
        nodeData+=NodeManager.compileObjectData(objects);
    }
}
