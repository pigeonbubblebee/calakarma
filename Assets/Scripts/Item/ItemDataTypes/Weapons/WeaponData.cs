using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "Calakarma/WeaponItem", order = 2)]
public class WeaponData : ItemData
{
    [Header("Weapon Settings")]
    public int damage;
    public float speed;
    public float chargeSpeed;
    public string[] otherAttributes;
    public WeaponType weaponType;
    public WeaponDamageType weaponDamageType;

    [Header("Weapon References")]
    public RuntimeAnimatorController weaponAnimator;

    // Enum Used To Determine Animation Type
    public enum WeaponType {
        Sword,
        Bow,
        Staff
    }

    // Enum Used To Determine Damage Type
    public enum WeaponDamageType {
        Melee,
        Ranged,
        Magic,
        Classless
    }
}
