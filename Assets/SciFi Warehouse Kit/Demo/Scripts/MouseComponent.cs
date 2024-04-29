using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Needed for UI event handling

public class MouseComponent : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool lockCursor = true;  // Controls whether the cursor is locked

    float xRotation = 0f;

    void Start()
    {
        // Initialize cursor state based on lockCursor flag
        UpdateCursorLock();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Optional: Unlock cursor when over UI elements if desired
            if (lockCursor) 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            return; // Skip camera control updates when over UI
        }

        if (lockCursor) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Check for input to toggle cursor locking
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
            UpdateCursorLock();
        }
    }

    void UpdateCursorLock()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
