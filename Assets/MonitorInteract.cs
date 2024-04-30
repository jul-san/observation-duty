using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorInteract : MonoBehaviour
{
    public CameraScreenBehavior cameraScreenBehavior;
    public float interactionDistance = 3f; // Distance within which the player can interact with the TV.
    public Transform player; // Assign the player's transform here.
    private bool showInteractionMessage = false; // Flag to control the display of the interaction message.

    // Reference to the CharacterController component of the player
    private CharacterController characterController;

    void Start()
    {
        // Get the CharacterController component from the player
        characterController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check the distance and whether the player is looking at the TV.
        if (Vector3.Distance(player.position, transform.position) < interactionDistance && IsPlayerLookingAtTV())
        {
            if (!cameraScreenBehavior.IsCameraViewActive())
            {
                // Only display the interaction message if not in camera view
                DisplayInteractionMessage();

                // Enter camera view on pressing 'comma'
                if (Input.GetKeyDown(KeyCode.Comma))
                {
                    DisablePlayerMovement();
                    cameraScreenBehavior.ActivateCameraView();
                }
            }
            else
            {
                // Display a different message or none at all
                // Ensure 'E' key exits the camera view
                if (Input.GetKeyDown(KeyCode.Comma))
                {
                    EnablePlayerMovement();
                    cameraScreenBehavior.DeactivateCameraView();
                }
            }
        }
        else if (cameraScreenBehavior.IsCameraViewActive())
        {
            // Player is already in camera view but not necessarily looking at TV anymore
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                EnablePlayerMovement();
                cameraScreenBehavior.DeactivateCameraView();
            }
        }
    }


    private void DisablePlayerMovement()
    {
        // Disable all possible movement controls here, including CharacterController, Rigidbody, or custom scripts.
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
    }

    private void EnablePlayerMovement()
    {
        if (characterController != null)
        {
            characterController.enabled = true;
        }

        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }

        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
    }

    private bool IsPlayerLookingAtTV()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                return true; // Player is looking at the TV.
            }
        }
        return false; // Player is not looking at the TV.
    }

    private void DisplayInteractionMessage()
    {
        showInteractionMessage = true; // Set the flag to true when the player can interact with the TV.
    }

    private void OnGUI()
    {
        /*if (cameraScreenBehavior.IsCameraViewActive())
        {
            // Always show exit text if in camera view
            Rect labelRect = new Rect(Screen.width / 2 - 100, Screen.height - 70, 200, 50);
            string message = "Press (E) to exit";
            GUI.Label(labelRect, message);
        }
        else if (showInteractionMessage)
        {
            // Show interaction message if not in camera view and near the TV
            Rect labelRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);
            string message = "Press (E) to monitor cameras";
            GUI.Label(labelRect, message);
        }*/
    }
}
 