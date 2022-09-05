using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool disabled = false;

    public float detectRadius;
    public bool detected = false;

    public virtual void updateMovement() {

    }

    void Update()
    {
        if(!detected) {
            this.GetComponent<Enemy>().flipping = false;
            Collider2D[] checkResults = 
            Physics2D.OverlapCircleAll((Vector2)transform.position, detectRadius);

            foreach(Collider2D c in checkResults) {
                GameObject checkedObject = c.gameObject;
                Player p = checkedObject.GetComponent<Player>();

                if(p!=null) {
                    detected = true;
                }
            }
        } 
    }
}
