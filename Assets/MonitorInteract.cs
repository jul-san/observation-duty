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
        // Reset the flag every frame.
        showInteractionMessage = false;

        // Check if the player is within the interaction distance and is looking at the TV.
        if (Vector3.Distance(player.position, transform.position) < interactionDistance && IsPlayerLookingAtTV())
        {
            DisplayInteractionMessage();

            // Check if the "E" key was pressed and the interaction message is being displayed.
            if (showInteractionMessage && Input.GetKeyDown(KeyCode.E))
            {
                if (!cameraScreenBehavior.IsCameraViewActive()) // If not in camera view, activate it
                {
                    // Disable player movement while in camera view
                    // Assuming you have a PlayerMovement script, you can disable it here
                    PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
                    if (movementScript != null)
                    {
                        movementScript.enabled = false;
                    }

                    cameraScreenBehavior.ActivateCameraView();
                }
                else // If already in camera view, deactivate it
                {
                    // Re-enable player movement
                    PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
                    if (movementScript != null)
                    {
                        movementScript.enabled = true;
                    }

                    cameraScreenBehavior.DeactivateCameraView();
                }
            }
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

    private void DisablePlayerMovement()
    {
        // Disable player movement by disabling the CharacterController
        if (characterController != null)
        {
            characterController.enabled = false;
        }
    }

    private void EnablePlayerMovement()
    {
        // Enable player movement by enabling the CharacterController
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }

    private void OnGUI()
    {
        if (cameraScreenBehavior.IsCameraViewActive())
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
        }
    }
}
 