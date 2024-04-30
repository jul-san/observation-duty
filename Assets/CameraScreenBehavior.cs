using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraScreenBehavior : MonoBehaviour
{
    public List<Camera> remoteCameras;
    public Camera playerCamera; // Declare playerCamera as a public Camera variable

    private int currentCameraIndex = 0;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchCamera(-1); // Switch to the previous camera
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
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
        Cursor.visible = true; // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can move freely

        isCameraViewActive = true;

        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
        else
        {
            Debug.LogError("Player camera reference is not assigned in the Inspector!");
        }

        // Enable the first remote camera
        if (remoteCameras.Count > 0)
        {
            remoteCameras[currentCameraIndex].enabled = true;
        }

        Debug.Log("Player Camera: " + playerCamera);
        Debug.Log("Current Remote Camera: " + remoteCameras[currentCameraIndex]);
    }

    public void DeactivateCameraView()
    {
        Debug.Log("Player Camera: " + playerCamera); // Log the playerCamera variable

        // Disable camera view and revert to default cursor
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true; // Ensure the cursor remains visible
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back if needed

        isCameraViewActive = false;

        // Disable all remote cameras
        foreach (var cam in remoteCameras)
        {
            cam.enabled = false;
        }

        // Re-enable the player camera if it's not null
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        else
        {
            Debug.LogError("Player camera is not assigned!"); // Log an error if playerCamera is null
        }

        Debug.Log("Player Camera: " + playerCamera);
        Debug.Log("Current Remote Camera: " + remoteCameras[currentCameraIndex]);
    }
}
