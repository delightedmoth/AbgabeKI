using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpTime;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public float jumpMultiplier = 4f;
    public float distanceforJump = 2f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Vector2 vecGravity;

    public bool jump;
    private bool isJumping;
    private float jumpCounter;
    public float fitness = 0f;
    private bool dead = false;
    private float distancetoObstacle1 = 10f;
    private float distancetoObstacle2 = 10f;
   

    private void Awake()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        groundCheck = rb.transform;
    }
    

    private void Update()
    {
      Debug.DrawRay(transform.position, Vector2.right * 9f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 9f,
            LayerMask.GetMask("Obstacle"));

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
       
        if (distanceforJump > distancetoObstacle1)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        jumpFunction();
        
        if (dead == false)
        {
            fitness += GameManager.Instance.gameSpeed * Time.deltaTime;
        }

    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position,
            new Vector2(0.5f, 1.05f), CapsuleDirection2D.Vertical, 0, groundLayer);
    }

    private void jumpFunction()
    {
        
        if (jump == true && isGrounded())
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

        if (jump == false)
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            dead = true;
        }
    }
    
    
}
