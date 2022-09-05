using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Calakarma/MaterialItem", order = 1)]
public class MaterialData : ItemData
{
    [Header("Material Settings")]
    public int sellPrice;
}
