
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    [Header("Bar References")]
    public Image currentBar;

    [Header("Bar Types")]
    [SerializeField]
    private Sprite charge;
    [SerializeField]
    private Sprite stealth;
    [SerializeField]
    private Sprite overload;

    WeaponData.WeaponDamageType weaponDamageType;

    // Updates Bar Color Based On Weapon Damage Type
    void Update()
    {
        if(weaponDamageType == WeaponData.WeaponDamageType.Melee) currentBar.sprite = charge;
        if(weaponDamageType == WeaponData.WeaponDamageType.Ranged) currentBar.sprite = stealth;
        if(weaponDamageType == WeaponData.WeaponDamageType.Magic) currentBar.sprite = overload;
        if(weaponDamageType == WeaponData.WeaponDamageType.Classless) {
            Color c = currentBar.color;
            c.a = 0f;
            currentBar.color = c;
        } else {
            Color c = currentBar.color;
            c.a = 1f;
            currentBar.color = c;
        }
    }

    // Setter Method For This Bar's Weapon Damage Type
    public void setWeaponDamageType(WeaponData.WeaponDamageType weaponDamageType) {
        this.weaponDamageType = weaponDamageType;
    }

    // Changes Amount In Bar
    public void changeAmount(float amount) {
        currentBar.fillAmount = amount;
    }
}
