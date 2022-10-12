using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "Calakarma/StaffItem", order = 5)]
public class StaffData : WeaponData
{
    [Header("Staff Settings")]
    public float projectileSpeed = 0.5f;
    public GameObject projectile;

    public int manaCost;

    public int overloadDamage;

    // Use Staff Attack
    public void onAttack(bool overloaded, LayerMask enemyLayer, Item equippedWeapon) {
        if(Player.Instance.playerStats.mana<manaCost) {
            return;
        }
        Player.Instance.playerStats.subtractMana(manaCost);
        if(!overloaded) { // Sets Damage According To Whether The Shot Is A Overload Shot Or Not
            projectile.GetComponent<Projectile>().damage = (int)(damage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100));
        }
        else {
            projectile.GetComponent<Projectile>().damage = (int)(overloadDamage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100));
            Player.Instance.playerStats.resetOverload();
        }
        Player.Instance.playerProjectileOrigin.fire(projectile, projectileSpeed); // Creates Projectile
    }
}
