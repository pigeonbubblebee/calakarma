using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : EnemyAttack
{
    public float chargeSpeed;
    public string chargeAnimatorName;

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
        e.animator.SetBool(chargeAnimatorName, true);
        invincibility = true;

        multiply = 1;

        if(Player.Instance.gameObject.transform.position.x < this.transform.position.x) {
            multiply = -1;
        }
    }

    public override void updateAttack(Enemy e, float attackTimestamp) {

        e.animator.SetBool(chargeAnimatorName, true);

        e.flipping = false;
        
        checkAttack(e);

        Rigidbody2D rb = e.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(multiply*chargeSpeed
                , rb.velocity.y);

        base.updateAttack(e, attackTimestamp);
    }

    public override void finishAttack(Enemy e) {
        e.animator.SetBool(chargeAnimatorName, false);

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
