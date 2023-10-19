using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Ground movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));

            // Check if the player is sprinting
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

            moveDirection = inputDir * currentSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);

        // Toggle sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            isSprinting = false;
        }
    }
}
