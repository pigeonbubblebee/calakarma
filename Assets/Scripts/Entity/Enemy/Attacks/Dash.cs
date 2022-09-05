using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : EnemyAttack
{
    public float dashSpeed;

    public float chargeTime;

    public string chargeAnimatorName;
    public string dashAnimatorName;
    int multiply;

    bool invincibility;

    [SerializeField]
    private Transform damagePoint;
    [SerializeField]
    private float damageRad;
    [SerializeField]
    int damage;

    public override void beginAttack(Enemy e) {
        e.setMovementActive(false);
        e.animator.SetBool("Walking", false);
        invincibility = true;
    }

    public override void updateAttack(Enemy e, float attackTimestamp) {
        if(Time.time <= attackTimestamp + chargeTime) { // Charging Dash
            multiply = 1;

            if(Player.Instance.gameObject.transform.position.x < this.transform.position.x) {
                multiply = -1;
            }

            e.animator.SetBool(chargeAnimatorName, true);
            e.animator.SetBool(dashAnimatorName, false);

            e.flipping = true;
        } else { // Dashing
            e.flipping = false;
            e.animator.SetBool(chargeAnimatorName, false);
            e.animator.SetBool(dashAnimatorName, true);

            checkAttack(e);

            Rigidbody2D rb = e.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(multiply*dashSpeed
                    , rb.velocity.y);
        }

        base.updateAttack(e, attackTimestamp);
    }

    public override void finishAttack(Enemy e) {
        e.animator.SetBool(chargeAnimatorName, false);
        e.animator.SetBool(dashAnimatorName, false);

        Rigidbody2D rb = e.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void checkAttack(Enemy e) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(damagePoint.position, damageRad);

        foreach(Collider2D hit in hits) {
            if(hit.GetComponent<Player>()!=null && invincibility) {
                Player.Instance.playerStats.takeDamage((int)(damage*(1+((float)e.getStatEffect("damagepercent"))/100)));
                invincibility = false;
            }
        }
    }

    public override bool canAttack(Enemy e) {
        return e.getDetected();
    }
}
