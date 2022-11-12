
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsPlayer : MonoBehaviour
{
    // Resets Speed To Normal
    public void resetSpeed() {
        Player.Instance.playerCombat.resetSpeed();
    }

    // Sets Speed To The Weapon Attack Speed
    public void setSpeed() {
        Player.Instance.playerCombat.setSpeedToWeaponSpeed();
    }

    // Sets Speed To Weapon Charging Up Speed
    public void setChargeSpeed() {
        Player.Instance.playerCombat.setSpeedToWeaponCharge();
    }

    // Sets Speed To Full Charge Attack Speed
    public void setChargingUpSpeed() {
        Player.Instance.playerCombat.setSpeedToWeaponChargeHundredPercent();
    }

    // Preforms A Sword Attack
    public void swordAttack() {
        Player.Instance.playerCombat.swordAttack();
    }

    // Preforms A Bow Attack
    public void bowAttack() {
        Player.Instance.playerCombat.bowAttack();
    }

    // Preforms A Staff Attack
    public void staffAttack() {
        Player.Instance.playerCombat.staffAttack();
    }

    // Forces Held Item's Sword Animation Tp Play
    public void playerSwordAnims() {
        Player.Instance.playerCombat.playWeaponAnimationsSword();
    }

    // Code Reads Attack As Done
    public void finishAttack() {
        if (!ControlBinds.GetButton("Attack")) // Checks For Attacks
        {
            Player.Instance.playerCombat.chargingAttack = false;
        }
    }

    // Resets Charge System
    public void resetCombo() {
        Player.Instance.playerStats.resetCombo();
    }

    // Resets Speed Mid Animation
    public void resetSpeedNoChangeAtkBool() {
        Player.Instance.playerCombat.resetSpeedNoChangeAttackingBool();
    }
}