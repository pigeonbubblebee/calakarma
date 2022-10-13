using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : EnemyAttack
{
    public float power;

    public float chargeTime;

    public string chargeAnimatorName;
    public string shootAnimatorName;

    [SerializeField]
    private Transform firePoint;
    
    [SerializeField]
    int damage;

    public float range;

    public GameObject projectile;

    public override void beginAttack(Enemy e) {
        e.setMovementActive(false);
        e.animator.SetBool("Walking", false);
    }

    public override void updateAttack(Enemy e, float attackTimestamp) {
        if(Time.time < attackTimestamp + chargeTime) { // Charging Dash
            e.animator.SetBool(chargeAnimatorName, true);
            e.animator.SetBool(shootAnimatorName, false);
        } else if(Time.time == attackTimestamp + chargeTime) {
            fire(e);
        } else { // Dashing
            e.animator.SetBool(chargeAnimatorName, false);
            e.animator.SetBool(shootAnimatorName, true);
        }

        base.updateAttack(e, attackTimestamp);
    }

    public virtual void fire(Enemy e) {
        projectile.GetComponent<Projectile>().damage = (int)(damage*(1+((float)e.getStatEffect("damagepercent"))/100));

        Vector2 position = firePoint.position;
        Vector2 playerPosition = (Vector2)Player.Instance.gameObject.transform.position;
        Vector2 direction = playerPosition - position;
        firePoint.right = direction;

        GameObject newProj = Instantiate(projectile, firePoint.position, firePoint.rotation);
        newProj.GetComponent<Rigidbody2D>().velocity = firePoint.right*power;
        newProj.SetActive(true);
    }

    public override void finishAttack(Enemy e) {
        e.animator.SetBool(chargeAnimatorName, false);
        e.animator.SetBool(shootAnimatorName, false);
    }

    public override bool canAttack(Enemy e) {
        float distanceFromPlayer = Mathf.Sqrt((e.transform.position.x-Player.Instance.transform.position.x)*(e.transform.position.x-Player.Instance.transform.position.x) + 
                (e.transform.position.y-Player.Instance.transform.position.y)*(e.transform.position.y-Player.Instance.transform.position.y));
        
        bool result = range >= distanceFromPlayer;
        return e.getDetected() && result;
    }
}
