using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ControlBinds
{
    static Dictionary<string, Bind> binds = new Dictionary<string, Bind>();

    static string[] defaultActions = new string[] {
        "Left",
        "Right",
        "Jump",
        "Dash",        
        "Inventory",
        "Escape",
        "Dialogue",
        "Interact",
        "Attack"
    };

    static Bind[] defaultBinds = new Bind[] {
        new Bind(KeyCode.A),
        new Bind(KeyCode.D),
        new Bind(KeyCode.Space),
        new Bind(KeyCode.R),
        new Bind(KeyCode.B),
        new Bind(KeyCode.Escape),
        new Bind(KeyCode.Z),
        new Bind(1),
        new Bind(0)
    };

    static ControlBinds() {
        InitializeDict();
    }

    public static string[] getDefaultActions() {
        return defaultActions;
    }

    private static void InitializeDict() {
        binds = new Dictionary<string, Bind>();

        for(int i = 0; i < defaultActions.Length; i++) {
            binds.Add(defaultActions[i], defaultBinds[i]);
        }
    }

    public static void SetBindMap(string bindMap, Bind key)
    {
        if (!binds.ContainsKey(bindMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + bindMap);
        binds[bindMap] = key;
    }

    public static Bind GetBindMap(string bindMap)
    {
        if (!binds.ContainsKey(bindMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + bindMap);
        return binds[bindMap];
    }
 
    public static bool GetButtonDown(string button)
    {
        if(binds[button].isKey)
            return Input.GetKeyDown(binds[button].key);
        else
            return Input.GetMouseButtonDown(binds[button].mouseButton);
    }

    public static bool GetButtonUp(string button)
    {
        if(binds[button].isKey)
            return Input.GetKeyUp(binds[button].key);
        else
            return Input.GetMouseButtonUp(binds[button].mouseButton);
    }

    public static bool GetButton(string button)
    {
        if(binds[button].isKey)
            return Input.GetKey(binds[button].key);
        else
            return Input.GetMouseButton(binds[button].mouseButton);
    }
}


