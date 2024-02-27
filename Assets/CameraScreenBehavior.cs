using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenBehavior : MonoBehaviour
{
    public List<Camera> remoteCameras;
    private int currentCameraIndex = 0;
    private Camera mainCamera;
    private bool isCameraViewActive = false;

    private void Start()
    {
        // Store reference to the main camera
        mainCamera = Camera.main;

        // Disable all remote cameras at start
        foreach (var cam in remoteCameras)
        {
            cam.enabled = false;
        }
    }

    private void Update()
    {
        if (isCameraViewActive && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    public void ActivateCameraView()
    {
        isCameraViewActive = true;

        // Disable the main camera
        mainCamera.enabled = false;

        // Enable the first remote camera
        if (remoteCameras.Count > 0)
        {
            remoteCameras[currentCameraIndex].enabled = true;
        }
    }

    public void SwitchCamera()
    {
        // Disable the current camera
        remoteCameras[currentCameraIndex].enabled = false;

        // Increment the index and wrap around if necessary
        currentCameraIndex = (currentCameraIndex + 1) % remoteCameras.Count;

        // Enable the next camera
        remoteCameras[currentCameraIndex].enabled = true;
    }

    public void DeactivateCameraView()
    {
        isCameraViewActive = false;

        // Disable all remote cameras
        foreach (var cam in remoteCameras)
        {
            cam.enabled = false;
        }

        // Re-enable the main camera
        mainCamera.enabled = true;
    }
}
