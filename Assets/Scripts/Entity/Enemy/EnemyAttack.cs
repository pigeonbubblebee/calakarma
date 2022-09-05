using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public float attackLength;
    public float attackCooldown;

    public virtual void beginAttack(Enemy e) {

    }

    public virtual void updateAttack(Enemy e, float attackTimestamp) {
        if(Time.time > attackTimestamp + attackLength) {
            e.finishAttack();
            this.finishAttack(e);
        }
    }

    public virtual void finishAttack(Enemy e) {

    }

    public virtual bool canAttack(Enemy e) {
        return false;
    }
}
