using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTextUI : MonoBehaviour
{
    TextMeshProUGUI textField;
    [Header("Text Settings")]
    public string key;

    // Localizes Text
    void Start() {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.getLocalizedValue(key);
        textField.text = value;
    }
}
