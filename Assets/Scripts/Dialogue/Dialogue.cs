using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Calakarma/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Dialogue Settings")]
    public string sourceName;
    public string[] dialogues;
    public Sprite sourceImage;
}