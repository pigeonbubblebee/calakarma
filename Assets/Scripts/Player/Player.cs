using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tymski;

public class Player : MonoBehaviour
{
    // Singleton Pattern
    private static Player _instance;
    public static Player Instance { get { return _instance; } }

    [Header("Player Parts")]
    public PlayerAnimation playerAnimation;
    public PlayerMovement playerMovement;
    public PlayerWorldInteract playerWorldInteract;
    public PlayerItemManager playerItemManager;
    public PlayerCombat playerCombat;
    public PlayerProjectileOrigin playerProjectileOrigin;
    public PlayerStats playerStats;
    public PlayerMemento playerMemento;
    public PlayerJewelry playerJewelry;

    public Camera mainCamera;

    public static string respawnScene;

    // Implements Singleton Pattern
    public void Awake()
    {
        if(respawnScene == null) {
            respawnScene = "Outskirts_0";
        }

        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        mainCamera = Camera.main;
    }

    // Makes Sure That Collisions Don't Happen Between Certain Things
    void Start() {
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.projectileLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.itemLayer);
        Physics2D.IgnoreLayerCollision(Layers.projectileLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.dialogueLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.projectileLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.projectileLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyProjectileLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyProjectileLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyProjectileLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyProjectileLayer, Layers.projectileLayer);

        Physics2D.IgnoreLayerCollision(Layers.dialogueLayer, Layers.enemyHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.enemyHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyHitboxLayer, Layers.enemyHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.enemyHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyHitboxLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyProjectileLayer, Layers.enemyHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.enemyHitboxLayer);

        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.projectileLayer);
        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.itemLayer);
        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.playerHitboxLayer);
        Physics2D.IgnoreLayerCollision(Layers.playerHitboxLayer, Layers.enemyHitboxLayer);
    }

    // Manually Resets Singleton
    public void setSingleton() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

    }
}
