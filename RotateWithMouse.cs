using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation

    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // Change in mouse position
            float rotationX = delta.y * rotationSpeed * Time.deltaTime; // Vertical mouse movement
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime; // Horizontal mouse movement

            transform.Rotate(rotationX, rotationY, 0, Space.World); // Rotate object globally
            lastMousePosition = Input.mousePosition;
        }
    }
}
