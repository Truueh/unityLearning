﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Cam;
    private float playerSpeed = 6f;
    private Rigidbody rb;
    private bool canJump = true;
    public bool isGrounded = false;
    public float jumpForce = 10;
    public float bonusGravity = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();


        // # DEBUG
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // handle movement
        HandleMovement();

        // handle jumps
        HandleJumps();
    }

    private void HandleMovement()
    {
        // handle basic movement
        Vector3 playerForwardVec = this.transform.forward * playerSpeed;
        Vector3 playerRightVec = this.transform.right * playerSpeed;
        Vector3 velocity = Vector3.zero;

        // check if player is sprinting
        if (Input.GetKey(KeyCode.LeftShift))
            playerSpeed = 10f;
        else
            playerSpeed = 6f;

        // handle movement input
        if (Input.GetKey(KeyCode.W))
            velocity += -1 * new Vector3(playerForwardVec.x, 0, playerForwardVec.z);
        if (Input.GetKey(KeyCode.S))
            velocity += new Vector3(playerForwardVec.x, 0, playerForwardVec.z);
        if (Input.GetKey(KeyCode.D))
            velocity += -1 * new Vector3(playerRightVec.x, 0, playerRightVec.z);
        if (Input.GetKey(KeyCode.A))
            velocity += new Vector3(playerRightVec.x, 0, playerRightVec.z);

        // cap player speed
        if (velocity.x > 10f)
        {
            float remainder = velocity.x - 10f;
            velocity.x = 10f;
            velocity.z -= remainder;
        }
        else if (velocity.z > 10f)
        {
            float remainder = velocity.z - 10f;
            velocity.z = 10f;
            velocity.x -= remainder;
        }
        else if (velocity.x < -10f)
        {
            float remainder = velocity.x + 10f;
            velocity.x = -10f;
            velocity.z += remainder;
        }
        else if (velocity.z < -10f)
        {
            float remainder = velocity.z + 10f;
            velocity.z = -10f;
            velocity.x += remainder;
        }

        // apply new velocity
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    private void HandleJumps()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity += new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        else if (!isGrounded)
        {
            rb.velocity -= new Vector3(0, bonusGravity * Time.deltaTime, 0);
        }
    }
}