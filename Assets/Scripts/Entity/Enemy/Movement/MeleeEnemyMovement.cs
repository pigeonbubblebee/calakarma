using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : EnemyMovement
{
    public float moveSpeed;
    int multiply;

    public float minDistFromPlayer;

    Rigidbody2D rb;
    Animator animator;
    Enemy e;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        e = gameObject.GetComponent<Enemy>();
    }

    public override void updateMovement() {
        if (disabled) { return; }

        if(detected) {
            e.flipping = true;
            multiply = 1;

            if(Player.Instance.gameObject.transform.position.x < this.transform.position.x) {
                multiply = -1;
            }

            float distanceFromPlayer = Mathf.Sqrt((this.transform.position.x-Player.Instance.transform.position.x)*(this.transform.position.x-Player.Instance.transform.position.x) + 
                (this.transform.position.y-Player.Instance.transform.position.y)*(this.transform.position.y-Player.Instance.transform.position.y));

            if(distanceFromPlayer > minDistFromPlayer) {

                rb.velocity = new Vector2(multiply*moveSpeed
                    , rb.velocity.y);

                animator.SetBool("Walking", true);

            } else {

                animator.SetBool("Walking", false);

            }
        }
    }

    
}
