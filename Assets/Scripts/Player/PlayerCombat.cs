using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Combat References")]
    public GameObject weaponObject;

    public Transform swordPoint;

    public LayerMask enemyLayer;

    public bool chargingAttack = false;
    private bool attacking = false;

    public Transform rotatingTowardsMouse;

    private Item equippedWeapon;

    // Sets Equipped Weapon
    public void setEquipped(Item equip)
    {
        equippedWeapon = equip;
    }

    // Returns The Equipped Weapon
    public Item getEquipped()
    {
        return equippedWeapon;
    }

    // Updates Combat System And Checks For Attacks
    void Update()
    {
        if (equippedWeapon != null) // Updates Bow Rotations
        {
            bowRotations();
        }

        if (equippedWeapon != null)
        {
            if (ControlBinds.GetButton("Attack")) // Checks For Attacks
            {
                Player.Instance.playerCombat.weaponObject.SetActive(true);
                Player.Instance.playerAnimation.setBool("Sprinting", false);
                
                chargingAttack = true;

                if (!UIManager.Instance.uiOpen())
                {
                    if (((WeaponData)(this.equippedWeapon.getItemData())).weaponType == WeaponData.WeaponType.Sword && !attacking)
                    { // Starts Sword Attack
                        // Player.Instance.playerStats.charging = true;

                        attacking = true;
                        Player.Instance.playerAnimation.setBool("ChargingSword", true);
                        weaponObject.GetComponent<Animator>().SetBool("Charging", true);
                    }

                    if (((WeaponData)(this.equippedWeapon.getItemData())).weaponType == WeaponData.WeaponType.Bow && !attacking)
                    { // Starts Bow Attack
                        attacking = true;
                        Player.Instance.playerAnimation.setBool("ChargingBow", true);
                        weaponObject.GetComponent<Animator>().SetBool("Charging", true);
                    }

                    if (((WeaponData)(this.equippedWeapon.getItemData())).weaponType == WeaponData.WeaponType.Staff && !attacking)
                    { // Starts Staff Attack
                        attacking = true;
                        //Debug.Log(chargingAttack);
                        Player.Instance.playerAnimation.setBool("ChargingStaff", true);
                        weaponObject.GetComponent<Animator>().SetBool("Charging", true);
                    }
                }
                
            }
            if (ControlBinds.GetButtonUp("Attack"))
            { // Stops Attack Animations And Resets Charge System
                Player.Instance.playerAnimation.setBool("ChargingSword", false);
                Player.Instance.playerAnimation.setBool("ChargingBow", false);
                Player.Instance.playerAnimation.setBool("ChargingStaff", false);
                weaponObject.GetComponent<Animator>().SetBool("Charging", false);

                
            }
        }
    }

    // Updates Bow Rotation Graphics
    private void bowRotations()
    {
        weaponObject.GetComponent<Animator>().runtimeAnimatorController =
                ((WeaponData)(equippedWeapon.getItemData())).weaponAnimator;

        if (((WeaponData)(this.equippedWeapon.getItemData())).weaponType == WeaponData.WeaponType.Bow && attacking)
        {
            if (rotatingTowardsMouse.eulerAngles.z > 0f && rotatingTowardsMouse.eulerAngles.z < 35f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 1);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 1);
            }
            if (rotatingTowardsMouse.eulerAngles.z > 35f && rotatingTowardsMouse.eulerAngles.z < 60f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 2);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 2);
            }
            if (rotatingTowardsMouse.eulerAngles.z > 60f && rotatingTowardsMouse.eulerAngles.z < 90f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 3);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 3);
            }
            if (rotatingTowardsMouse.eulerAngles.z > 90f && rotatingTowardsMouse.eulerAngles.z < 120f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 3);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 3);
            }
            if (rotatingTowardsMouse.eulerAngles.z > 120f && rotatingTowardsMouse.eulerAngles.z < 155f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 2);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 2);
            }
            if (rotatingTowardsMouse.eulerAngles.z > 155f && rotatingTowardsMouse.eulerAngles.z < 180f)
            {
                weaponObject.GetComponent<Animator>().SetInteger("ChargeRotate", 1);
                Player.Instance.playerAnimation.setInt("ChargeRotate", 1);
            }
        }
    }

    // Starts Sword Attack
    public void swordAttack()
    {
        ((SwordData)(equippedWeapon.getItemData())).onAttack(Player.Instance.playerStats.comboReady, enemyLayer, swordPoint, equippedWeapon);
    }

    // Starts Bow Attack
    public void bowAttack()
    {
        ((BowData)(equippedWeapon.getItemData())).onAttack(Player.Instance.playerStats.stealthed, enemyLayer, equippedWeapon);
    }

    // Starts Staff Attack
    public void staffAttack()
    {
        ((StaffData)(equippedWeapon.getItemData())).onAttack(Player.Instance.playerStats.overloaded, enemyLayer, equippedWeapon);
    }

    // Resets Animator Speed
    public void resetSpeed()
    {
        attacking = false;
        Player.Instance.playerAnimation.animator.speed = 1f;
        weaponObject.GetComponent<Animator>().speed = 1f;
    }

    // Sets Speed To Weapon Attack Speed
    public void setSpeedToWeaponSpeed()
    {
        Player.Instance.playerAnimation.animator.speed = 1 / ((WeaponData)(equippedWeapon.getItemData())).speed;
        weaponObject.GetComponent<Animator>().speed = 1 / ((WeaponData)(equippedWeapon.getItemData())).speed;
    }

    // Sets Speed To Weapon Charge Speed
    public void setSpeedToWeaponCharge()
    {
        Player.Instance.playerAnimation.animator.speed = 1 / ((WeaponData)(equippedWeapon.getItemData())).chargeSpeed;
        weaponObject.GetComponent<Animator>().speed = 1 / ((WeaponData)(equippedWeapon.getItemData())).chargeSpeed;
    }

    // Sets Speed To Weapon Full Charge Speed
    public void setSpeedToWeaponChargeHundredPercent()
    {
        Player.Instance.playerAnimation.animator.speed = 1 / ((SwordData)(equippedWeapon.getItemData())).charge100PercentSpeed;
        weaponObject.GetComponent<Animator>().speed = 1 / ((SwordData)(equippedWeapon.getItemData())).charge100PercentSpeed;
    }

    // Resets Animator Speed Without Changing Attacking Bool
    public void resetSpeedNoChangeAttackingBool()
    {
        Player.Instance.playerAnimation.animator.speed = 1f;
        weaponObject.GetComponent<Animator>().speed = 1f;
    }

    // Forces Held Item To Play Sword Animation
    public void playWeaponAnimationsSword()
    {
        Player.Instance.playerAnimation.setBool("ChargingSword", false);
        weaponObject.GetComponent<Animator>().SetBool("Charging", false);
    }
}
