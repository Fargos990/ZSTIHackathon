using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public float sensitivity = 2.0f; // Mouse sensitivity
    [SerializeField] Transform playerBody; // The player's body or character controller
    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotate the camera based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Clamp the vertical rotation

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotate the camera vertically
        playerBody.Rotate(Vector3.up * mouseX); // Rotate the player horizontally
    }
}
