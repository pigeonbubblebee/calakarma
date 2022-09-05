
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("UI Manager References")]
    GameObject[] ui;
    public UIParent uiParent;
    public Texture2D[] mouses;

    // Singleton Pattern
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    // Keys
    public static readonly int Inventory = 0;
    public static readonly int DialogueBox = 1;
    public static readonly int EscMenu = 2;

    // Mouse Finals
    public static readonly int Default_Mouse = 0;
    public static readonly int Dialogue_Mouse = 1;
    public static readonly int Exit_Mouse = 2;

    // Floating Text References
    public GameObject floatingText;

    // Initializes UI Manager
    void Awake()
    {
        // Implements Singleton Pattern
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        this.ui = uiParent.ui;

        // Hides All UI
        foreach(GameObject o in ui) {
            o.SetActive(false);
        }

        // Sets Mouse To Default
        changeMouse(Default_Mouse);

        
    }

    // Gets Player Input And Handles UI Based On It
    void Update()
    {
        getPlayerInput();
    }

    private void getPlayerInput() {
        if(ControlBinds.GetButtonDown("Inventory")) {
            if(!uiOpen() || uiOpen(Inventory)) {
                toggleUI(Inventory);
            }

            if(uiOpen(Inventory)) {
                changeMouse(Default_Mouse);
            }
        }

        if(ControlBinds.GetButtonDown("Escape")) {
            if(uiOpen(Inventory)) {
                toggleUIOff(Inventory);
            } else {
                if(!(uiOpen(DialogueBox))) {
                    toggleUI(EscMenu);
                }
            }
        }
    }

    // Toggles Designated UI From On/Off
    public void toggleUI(int ui) {
        if(uiOpen() && !this.ui[ui].activeInHierarchy) {
            return;
        }

        this.ui[ui].SetActive(!this.ui[ui].activeInHierarchy);
    }

    // Turns Designated UI On
    public void toggleUIOn(int ui) {
        this.ui[ui].SetActive(true);
    }

    // Turns Designated UI Off
    public void toggleUIOff(int ui) {
        this.ui[ui].SetActive(false);
    }

    // Checks If UI Is Open
    public bool uiOpen() {
        foreach(GameObject o in ui) {
            if(o.activeInHierarchy) {
                return true;
            }
        }
        return false;
    }

    // Overload To Check If A Specific UI Is Open
    public bool uiOpen(int ui) {
        return this.ui[ui].activeInHierarchy;
    }

    // Turns Off All UI
    public void turnOffAllUI() {
        foreach(GameObject o in ui) {
            o.SetActive(false);
        }
    }

    // Changes Mouse Cursor To Designated Mouse
    public void changeMouse(int mouse) {
        Cursor.SetCursor(mouses[mouse], Vector2.zero, CursorMode.Auto);
    }

    // Creates A Instance Of Floating Text
    public void createFloatingText(Vector2 position, string text, float lifeSpan) {
        GameObject newText = Instantiate(floatingText, position, Quaternion.identity);

        newText.SetActive(true);
        TextMeshPro textMesh = newText.GetComponent<TextMeshPro>();
        textMesh.text = text;
        Destroy(newText, lifeSpan);
        
    }
}
