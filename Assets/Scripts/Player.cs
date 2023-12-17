using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    public float jumpTime;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public float jumpMultiplier = 4f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Vector2 vecGravity;

    private bool isJumping;
    private float jumpCounter;

    private void Awake()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        direction = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetButton("Jump") && isGrounded()) 
        {
            rb.velocity = new Vector2(0, jumpForce);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y * 0.6f);
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * gravity * Time.deltaTime;
        }

    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position,
            new Vector2(0.5f, 1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.gameOver();
        }
        else
        {
            Debug.Log("not triggered " + other.name);
        }
    }
}
