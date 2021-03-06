﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private SpriteRenderer sprtRndr;

    public Sprite spriteMovingRight;
    public Sprite spriteMovingLeft;

    public float playerMoveSpeed;
    public float playerJumpHeigth;

    private float BASE_MOVE_SPEED = 3;
    private float BASE_JUMP_HEIGTH = 8;

    private double timer;
    private double DIRECTION_REVERSE_COOLDOWN_TIME = 0.5;
    private bool isOnDirectionReverseCooldown;

    public PauseMenu pauseMenuScript;

    //these variables help ensure player can only jump while touching ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool onGround;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprtRndr = GetComponent<SpriteRenderer>();

        playerMoveSpeed = BASE_MOVE_SPEED;
        playerJumpHeigth = BASE_JUMP_HEIGTH;

        timer = 0;
        isOnDirectionReverseCooldown = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // rotates player
        //transform.Rotate(new Vector3(0, 0, -200) * Time.deltaTime);
        //RotateLeft();

        playerMovement();
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        playerJump();

        if(isOnDirectionReverseCooldown)
        {
            timer += Time.deltaTime;

            if(timer >= DIRECTION_REVERSE_COOLDOWN_TIME)
            {
                timer = 0;
                isOnDirectionReverseCooldown = false;
            }
        }

	}

    void FixedUpdate()
    {
        //playerMovement();
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward * -90);
    }

    void playerMovement()
    {
        rb2d.velocity = new Vector2(transform.localScale.x * playerMoveSpeed, rb2d.velocity.y);
        //rb2d.velocity = new Vector2(playerMoveSpeed, rb2d.velocity.y);
        //rb2d.velocity = new Vector2(1, 0);

        /*
        float movementHorizontal = playerMoveSpeed;
        float movementVertical = 0;
        Vector2 movement = new Vector2(movementHorizontal, movementVertical);
        rb2d.AddForce(movement);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("Wall") && !isOnDirectionReverseCooldown)
        {
            reversePlayerDirection();
            isOnDirectionReverseCooldown = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            //TODO: Implement Level Completion Transistion here!!

            //temp victory acknowledgement
            Debug.Log("Level Completed");
            pauseMenuScript.levelComplete();
        }
    }

    void playerJump()
    {
        //LMB same as tapping anywhere on phone??
        if(Input.GetMouseButtonDown(0) && onGround)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, playerJumpHeigth);

            //rb2d.AddForce(Vector2.up * playerJumpHeigth);
            //rb2d.AddForce(new Vector2(0, playerJumpHeigth));
            //rb2d.AddForce(new Vector2(0, playerJumpHeigth), ForceMode2D.Impulse);
        }
    }

    void reversePlayerDirection()
    {
        playerMoveSpeed = playerMoveSpeed * -1;

        //Debug.Log("playerMoveSpeed = " + playerMoveSpeed);

        //if moving in the right direction
        if(playerMoveSpeed >= 0)
        {
            sprtRndr.sprite = spriteMovingRight;
        }
        //else moving in the left direction
        else
        {
            sprtRndr.sprite = spriteMovingLeft;
        }
    }
}
