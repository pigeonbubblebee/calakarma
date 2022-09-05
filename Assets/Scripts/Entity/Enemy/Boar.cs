
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    // [Header("Boar Settings")]
    // public float detectRadius;
    // bool detected = false;

    // public float moveSpeed;
    // public float dashMultiplier;

    // private float dashTimestamp = 0.0f;
    // public float dashRate = 1f;
    // public float maxDash = 10;

    // [SerializeField]
    // private int combatDamage;

    // int startingCombatDamage;
    // [SerializeField]
    // private Transform damagePoint;
    // [SerializeField]
    // private float damageRad;

    // public float minDistFromPlayer;

    // [SerializeField]
    // private float dash;

    // bool direction;
    // public Transform sprite;

    // private bool canAttack = true;

    // private int prevMultiply;

    // public override void onDamage(int damage) {
    //     detected = true;
    // }

    // new void Awake()
    // {
    //     base.Awake();
    //     startingCombatDamage = combatDamage;
    //     this.minDistFromPlayer *= Random.Range(0.8f, 1.2f);
    // }

    // // Checks Detection and Uses Basic Boar AI
    // void FixedUpdate()
    // {
    //     if(!detected) {
    //         Collider2D[] checkResults = 
    //         Physics2D.OverlapCircleAll((Vector2)transform.position, detectRadius);

    //         foreach(Collider2D c in checkResults) {
    //             GameObject checkedObject = c.gameObject;
    //             Player p = checkedObject.GetComponent<Player>();

    //             if(p!=null) {
    //                 detected = true;
    //             }
    //         }
    //     } else {
    //         Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

    //         int multiply = 1;

    //         if(Player.Instance.gameObject.transform.position.x < this.transform.position.x) {
    //             multiply = -1;
    //         }

    //         if(!canAttack) {
    //             multiply = 0-multiply;
    //             if(dash >= maxDash/3) canAttack = true;
    //         }

    //         if(dash>=maxDash) {
    //             rb.velocity = new Vector2(prevMultiply*moveSpeed*dashMultiplier
    //                 , rb.velocity.y);
    //             checkAttack();
    //             animator.SetBool("Walking", false);
    //             animator.SetBool("Charging", true);
    //         } else {
    //             float distanceFromPlayer = Mathf.Sqrt((this.transform.position.x-Player.Instance.transform.position.x)*(this.transform.position.x-Player.Instance.transform.position.x) + 
    //                 (this.transform.position.y-Player.Instance.transform.position.y)*(this.transform.position.y-Player.Instance.transform.position.y));

    //             if(distanceFromPlayer > minDistFromPlayer) {

    //                 rb.velocity = new Vector2(multiply*moveSpeed
    //                     , rb.velocity.y);

    //                 animator.SetBool("Walking", true);
    //                 animator.SetBool("Charging", false);
    //             } else {
    //                 animator.SetBool("Walking", false);
    //                 animator.SetBool("Charging", false);
    //             }
    //         }

    //         if(dash<maxDash)    prevMultiply = multiply;
    //     }

    //     updateDash();
    //     flipCheck();
    // }

    // // Updates Dashing Attack
    // private void updateDash() {
    //     if(Time.time > dashTimestamp)  {
    //         dashTimestamp = Time.time+(dashRate*Random.Range(0.8f, 1.2f));
    //         if(dash<maxDash)    { dash++; }
    //         else { dash-=maxDash; }
    //     }
    // }

    // // Checks For Player In Attack Range
    // private void checkAttack() {
    //     Collider2D[] hits = Physics2D.OverlapCircleAll(damagePoint.position, damageRad);

    //     foreach(Collider2D hit in hits) {
    //         if(hit.GetComponent<Player>()!=null && canAttack) {
    //             Player.Instance.playerStats.takeDamage(combatDamage);
    //             dash = 0;
    //             canAttack = false;
    //         }
    //     }
    // }

    // // Flips Entity To Make It Point Towards Player
    // private void flipCheck() {
    //     if(Player.Instance.transform.position.x>this.gameObject.transform.position.x&&!direction) {
    //         flip();
    //     } else if(Player.Instance.transform.position.x<this.gameObject.transform.position.x&&direction) {
    //         flip();
    //     }  
    // }

    // // Flips Sprite By 180 Degrees
    // private void flip() {
    //     direction=!direction;
    //     sprite.Rotate(0f, 180f, 0f);
    // }

    // public override void updateStats() {
    //     base.updateStats();
    //     this.combatDamage = (int)(startingCombatDamage*(1+((float)getStatEffect("damagepercent"))/100));
    // }
}