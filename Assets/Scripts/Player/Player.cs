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
            respawnScene = "Outskirts_1";
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
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.itemLayer);
        Physics2D.IgnoreLayerCollision(Layers.projectileLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.dialogueLayer);
        Physics2D.IgnoreLayerCollision(Layers.dialogueLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.enemyLayer);
        Physics2D.IgnoreLayerCollision(Layers.defaultLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.projectileLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.enemyLayer, Layers.exitLayer);
        Physics2D.IgnoreLayerCollision(Layers.itemLayer, Layers.exitLayer);
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
