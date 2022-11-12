
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]

    public float moveSpeed;
    public float jumpForce;

    private bool jumping = false;
    private bool dashing = false;

    private Rigidbody2D rb;
    private bool direction = false;

    private float moveDirection;

    [Header("Player Movement References")]

    public Transform playerSprite;
    public Transform weaponSprite;

    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private Transform groundCheck;

    public LayerMask groundMask;

    private bool grounded;
    public float checkRadius;

    // private int jumpCount;
    public float maxJump;
    float jumpTimestamp;

    public PlayerAnimation playerAnimation;

    public bool moving;

    private float mousePositionX;

    float dashTimeStamp;
    float moveDirectionNonZero;

    // Initializes RigidBody
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Gets Player Input And Flips Player
    void Update()
    {
        getInput();
        if(!Player.Instance.playerCombat.chargingAttack) {
            flipCheck();
        } else {
            flipCheckMouse();
        }
    }

    // Sets JumpCount To Correct Amount
    void Start()
    {
        // jumpCount = 1;
    }

    // Updates Grounded And Moves Player Based On Inputs
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);
        if(grounded) {
            // jumpCount = 1;
            jumpTimestamp = Time.time;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        move();

        mousePositionX = Player.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
    }

    // Updates Input
    private void getInput() {
        moveDirection = 0;

        if(ControlBinds.GetButton("Left")) {
            moveDirection = -1;
        }
        if(ControlBinds.GetButton("Right")) {
            moveDirection = 1;
        }

        if(moveDirection != 0) {
            moveDirectionNonZero = moveDirection;
        }

        if(ControlBinds.GetButton("Jump")) {
            if(Time.time < jumpTimestamp + maxJump) jumping = true;
            else jumping = false;
        } else {
            jumping = false;
        }

        if(ControlBinds.GetButtonDown("Dash")&&Player.Instance.playerStats.dashReady) {
            dashing = true;
            dashTimeStamp = Time.time;
        }
    }

    // Flips Player To Make It Point Towards Mouse
    private void flipCheckMouse() {
        if(mousePositionX>Player.Instance.transform.position.x&&!direction) {
            flip();
        } else if(mousePositionX<Player.Instance.transform.position.x&&direction) {
            flip();
        }  
    }

    // Flips Player Based On Which Direction It Is Moving
    private void flipCheck() {
        if(moveDirection>0 && !direction) {
            flip();
        } else if(moveDirection < 0 && direction) {
            flip();
        }
    }

    // Flips Player Sprite By 180 Degrees
    private void flip() {
        direction=!direction;
        playerSprite.Rotate(0f, 180f, 0f);
        weaponSprite.Rotate(0f, 180f, 0f);
    }

    // Moves Player Based On Move Direction And Speed
    private void move() {
        rb.velocity = new Vector2(moveDirection*moveSpeed, rb.velocity.y);
        if(rb.velocity.x!=0) {
            moving = true;
            
            if(!Player.Instance.playerCombat.chargingAttack) {
                Player.Instance.playerCombat.weaponObject.SetActive(false);
                playerAnimation.setBool("Sprinting", true);
            } else {
                Player.Instance.playerCombat.weaponObject.SetActive(true);
            }
        } else {
            moving = false;
            playerAnimation.setBool("Sprinting", false);
            Player.Instance.playerCombat.weaponObject.SetActive(true);
        }
        if(jumping) { // Handles Jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*10f);
            // jumpCount--;
        }
        if(dashing) { // Handles Dashing
            rb.AddForce(new Vector2(Player.Instance.playerStats.dashSpeed*100f*moveDirectionNonZero, 0f));
            Player.Instance.playerStats.resetDash();
        }
        
        if(Time.time > dashTimeStamp+0.3f) {
            dashing = false;
        }
    }
}
