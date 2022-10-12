using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : Projectile
{
    // Makes Sure Arrow Is Facing The Right Direction
    public override void updateProj()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; // Gets Angle Based On Arrow Velocity Vector
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Arrow Base On Hit Functionality
    public override void onCollision(Collision2D collision)
    {
        if(collision.gameObject.layer == Layers.groundLayer || collision.gameObject.tag == "Player") { // Checks Ground And Enemy Layer
            rb.velocity = Vector2.zero; // Cancels All Velocity
            if(collision.gameObject.tag == "Player") { // Deals Damage To Enemy
                onHit(collision.gameObject);
            }

            Destroy(this.gameObject); // Terminates This Arrow's GameObject
        }
    }

    // Overrode Method
    // Deals Appropriate Damage To An Player Entity
    public virtual void onHit(GameObject player) {
        Player.Instance.playerStats.takeDamage(damage);
    }
}
