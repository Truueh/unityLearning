﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Cam;
    private float playerSpeed = 6f;
    private Rigidbody rb;
    private float jumpForce = 5000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // handle movement
        HandleMovement();

        // handle jumps
        HandleJupms();
    }

    private void HandleMovement()
    {
        // handle basic movement
        Vector3 cameraForwardVec = Cam.transform.forward * playerSpeed;
        Vector3 velocity = Vector3.zero;

        // check if player is sprinting
        if (Input.GetKey(KeyCode.LeftShift))
            playerSpeed = 10f;
        else
            playerSpeed = 6f;

        // handle movement input
        if (Input.GetKey(KeyCode.W))
            velocity += new Vector3(cameraForwardVec.x, 0, cameraForwardVec.z);
        if (Input.GetKey(KeyCode.S))
            velocity += -1 * new Vector3(cameraForwardVec.x, 0, cameraForwardVec.z);
        if (Input.GetKey(KeyCode.D))
            velocity += new Vector3(Cam.transform.right.x, 0, Cam.transform.right.z) * playerSpeed;
        if (Input.GetKey(KeyCode.A))
            velocity += -1 * new Vector3(Cam.transform.right.x, 0, Cam.transform.right.z) * playerSpeed;

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
        rb.velocity = velocity;
    }

    private void HandleJupms()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += new Vector3(0, 100, 0);   
        }

        if (Physics.CheckSphere(new Vector3(0, 10f, 0), 1))
        {
            print("touching ground");
        }
    }
}
