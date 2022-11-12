using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "WeaponData", menuName = "Calakarma/SwordItem", order = 3)]
public class SwordData : WeaponData
{
    [Header("Sword Settings")]
    public float reach = 0.5f;
    public float hitRad = 0.5f;
    
    public float charge100PercentSpeed;
    public int comboDamage;

    // Use Sword Attack
    public void onAttack(bool combo, LayerMask enemyLayer, Transform swordPoint, Item equippedWeapon) {
        swordPoint.localPosition = new Vector2(((SwordData)(equippedWeapon.getItemData())).reach, Player.Instance.transform.position.y+0.167f); // Moves Scan Radius To Correct Spot

        Collider2D[] hits = Physics2D.OverlapCircleAll(swordPoint.position, 
             ((SwordData)(equippedWeapon.getItemData())).hitRad, enemyLayer); // Scans All Sword Hits

        foreach(Collider2D collider in hits) { // Loops Through Each Hit Object And Applies Damage
            Enemy e = collider.gameObject.GetComponent<Enemy>();
            if(e!=null) {
                if(!combo) // Applies Appropriate Damage
                    e.takeDamage((int)(damage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100)));
                else
                    e.takeDamage((int)(comboDamage*(1+((float)Player.Instance.playerStats.getStatBonus("damagepercent"))/100)));

                

                playerMementoEffects(collider);

                if(!combo) {
                    Player.Instance.playerStats.addCombo(1);
                } else {
                    Player.Instance.playerStats.resetCombo();
                }
            }
        }
    }

    private void playerMementoEffects(Collider2D collision) {
        PlayerMemento p = Player.Instance.playerMemento;
        foreach(Item i in p.getEquippedMementos()) {
            if(i!=null)
                ((MementoData) (i.getItemData())).onMeleeCollision(collision, Player.Instance.playerMemento.getEmotionLevel(i.getItemData().id));
        }
    }
}
