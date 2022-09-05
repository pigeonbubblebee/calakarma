using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind {
    public bool isKey;
    public KeyCode key;
    public int mouseButton;

    public Bind(KeyCode x) {
        key = x;
        isKey = true;
    }

    public Bind(int x) {
        mouseButton = x;
        isKey = false;
    }
}