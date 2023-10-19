using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;
    public float airControlFactor = 0.1f;
    private Rigidbody rb;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isSprinting = false;

    private bool readyToJump;
    float jumpCooldown = .2f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        readyToJump = true;
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
        else
        {
            // Air movement (reduced control)
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));

            float currentSpeed = walkSpeed * airControlFactor; // Reduce speed for air control

            // Accumulate air control input
            moveDirection += inputDir * currentSpeed;

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;
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
