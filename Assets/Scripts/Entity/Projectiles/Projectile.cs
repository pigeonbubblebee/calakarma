using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [Header("Projectile References")]
    public Rigidbody2D rb;

    [Header("Projectile Settings")]
    public int damage;

    [SerializeField]
    private float maxLifetime = 10f;

    void Awake() // Sets Decay Timer
    {
        Destroy(this, maxLifetime);
    }
    
    void Start() { // Gets RigidBody From Object
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        updateProj(); // Calls Overrode Method
    }

    // Overrode Method To Act As Proxy For Update
    public virtual void updateProj() {

    }

    // Overrode Method Triggered When Object Collides
    public virtual void onCollision(Collision2D collision) {

    }
    
    // Calls onCollision
    void OnCollisionEnter2D(Collision2D other)
    {
        onCollision(other);
    }
}
