using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
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
        if(collision.gameObject.layer == Layers.groundLayer || collision.gameObject.layer == Layers.enemyLayer) { // Checks Ground And Enemy Layer
            rb.velocity = Vector2.zero; // Cancels All Velocity
            if(collision.gameObject.layer == Layers.enemyLayer) { // Deals Damage To Enemy
                onHit(collision.gameObject);
                playerMementoEffects(collision);
            }

            Destroy(this.gameObject); // Terminates This Arrow's GameObject
        }
    }

    // Overrode Method
    // Deals Appropriate Damage To An Enemy Entity
    public virtual void onHit(GameObject enemy) {
        enemy.GetComponent<Enemy>().takeDamage(damage);
    }

    private void playerMementoEffects(Collision2D collision) {
        PlayerMemento p = Player.Instance.playerMemento;
        foreach(Item i in p.getEquippedMementos()) {
            if(i!=null)
                ((MementoData) (i.getItemData())).onProjectileCollision(collision, Player.Instance.playerMemento.getEmotionLevel(i.getItemData().id));
        }
    }
}
