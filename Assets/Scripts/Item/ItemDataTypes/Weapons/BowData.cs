using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "Calakarma/BowItem", order = 4)]
public class BowData : WeaponData {
    [Header("Bow Settings")]
    public float projectileSpeed = 0.5f;
    public GameObject projectile;

    public int stealthDamage;

    //  Use Bow Attack
    public void onAttack(bool stealth, LayerMask enemyLayer, Item equippedWeapon) {
        if(!stealth) { // Sets Damage According To Whether The Shot Is A Stealth Shot Or Not
            projectile.GetComponent<Projectile>().damage = (int)(damage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100));
        } else {
            projectile.GetComponent<Projectile>().damage = (int)(stealthDamage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100));
            Player.Instance.playerStats.resetStealth();
        }
        Player.Instance.playerProjectileOrigin.fire(projectile, projectileSpeed); // Creates Projectile
    }
}
