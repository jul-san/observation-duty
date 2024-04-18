using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraScreenBehavior : MonoBehaviour
{
    public List<Camera> remoteCameras;
    public GameObject playerCamera; // Declare playerCamera as a public variable
    private int currentCameraIndex = 0;
    private Camera mainCamera;
    private bool isCameraViewActive = false;
        public Texture2D defaultCursor;
    public Texture2D cameraCursor;

    public bool IsCameraViewActive()
    {
        return isCameraViewActive;
    }

    private void Update()
    {
        if (isCameraViewActive)
        {
            // Switch cameras with A and D keys
            if (Input.GetKeyDown(KeyCode.A))
            {
                SwitchCamera(-1); // Switch to the previous camera
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SwitchCamera(1); // Switch to the next camera
            }
        }
    }

    private void SwitchCamera(int direction)
    {
        // Disable the current camera
        remoteCameras[currentCameraIndex].enabled = false;

        // Move the index by the specified direction and wrap around if necessary
        currentCameraIndex = (currentCameraIndex + direction + remoteCameras.Count) % remoteCameras.Count;

        // Enable the new camera
        remoteCameras[currentCameraIndex].enabled = true;
    }

public void ActivateCameraView()
{
    Cursor.SetCursor(cameraCursor, Vector2.zero, CursorMode.Auto);
    Cursor.visible = true;  // Make sure the cursor is visible
    Cursor.lockState = CursorLockMode.None;  // Unlock the cursor so it can move freely
    isCameraViewActive = true;

    // Find the main camera in the scene
    mainCamera = Camera.main;

    // If Camera.main doesn't return a valid camera, try to find one manually
    if (mainCamera == null)
    {
        Debug.LogWarning("Main camera reference not found. Searching for a camera manually...");
        mainCamera = FindObjectOfType<Camera>();
    }

    if (mainCamera != null)
    {
        mainCamera.enabled = false;
    }
    else
    {
        Debug.LogError("Main camera reference is not assigned in the Inspector!");
    }

    // Enable the first remote camera
    if (remoteCameras.Count > 0)
    {
        remoteCameras[currentCameraIndex].enabled = true;
    }

    Debug.Log("Main Camera: " + mainCamera);
    Debug.Log("Current Remote Camera: " + remoteCameras[currentCameraIndex]);
}

public void DeactivateCameraView()
{
    Debug.Log("Player Camera: " + playerCamera); // Log the playerCamera variable

    // Disable camera view and revert to default cursor
    Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    Cursor.visible = true;  // Ensure the cursor remains visible
    Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back if needed
    isCameraViewActive = false;

    // Disable all remote cameras
    foreach (var cam in remoteCameras)
    {
        cam.enabled = false;
    }

    // Re-enable the main camera if it's not null
    if (mainCamera != null)
    {
        mainCamera.enabled = true;
    }
    else
    {
        Debug.LogError("Main camera is not assigned!"); // Log an error if mainCamera is null
    }
    Debug.Log("Main Camera: " + mainCamera);
    Debug.Log("Current Remote Camera: " + remoteCameras[currentCameraIndex]);
}
}
