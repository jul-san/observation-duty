using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenBehavior : MonoBehaviour
{
    public List<Camera> remoteCameras;
    public GameObject playerCamera; // Declare playerCamera as a public variable
    private int currentCameraIndex = 0;
    private Camera mainCamera;
    private bool isCameraViewActive = false;

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
    isCameraViewActive = true;

    // Find the main camera in the scene
    mainCamera = Camera.main;

    // If Camera.main doesn't return a valid camera, try to find one manually
    if (mainCamera == null)
    {
        Debug.LogWarning("Main camera reference not found. Searching for a camera manually...");
        mainCamera = FindObjectOfType<Camera>();
    }

    // If no camera is found, log a warning
    if (mainCamera == null)
    {
        Debug.LogWarning("No main camera found in the scene. Camera switching may not work correctly.");
    }
    else
    {
        // Disable the main camera
        mainCamera.enabled = false;
    }

    // Enable the first remote camera
    if (remoteCameras.Count > 0)
    {
        remoteCameras[currentCameraIndex].enabled = true;
    }

    // Disable the player movement
    if (playerCamera != null)
    {
        Rigidbody playerRigidbody = playerCamera.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on playerCamera GameObject!");
        }
    }
    else
    {
        Debug.LogError("Player camera is not assigned!"); // Log an error if playerCamera is null
    }
}

public void DeactivateCameraView()
{
    Debug.Log("Player Camera: " + playerCamera); // Log the playerCamera variable

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

    // Reactivate the player camera if it's not null
    if (playerCamera != null)
    {
        // Re-enable the player movement
        Rigidbody playerRigidbody = playerCamera.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on playerCamera GameObject!");
        }
    }
    else
    {
        Debug.LogError("Player camera is not assigned!"); // Log an error if playerCamera is null
    }
}
}
