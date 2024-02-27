using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MonitorInteract : MonoBehaviour
{
    public CameraScreenBehavior cameraScreenBehavior;
    public float interactionDistance = 3f; // Distance within which the player can interact with the TV.
    public Transform player; // Assign the player's transform here.
    private bool showInteractionMessage = false; // Flag to control the display of the interaction message.

    private void Update()
    {
        // Reset the flag every frame.
        showInteractionMessage = false;

        // Check if the player is within the interaction distance and is looking at the TV.
        if (Vector3.Distance(player.position, transform.position) < interactionDistance && IsPlayerLookingAtTV())
        {
            DisplayInteractionMessage();
        }

        if (showInteractionMessage && Input.GetKeyDown(KeyCode.E))
        {
            cameraScreenBehavior.ActivateCameraView(); 
        }

    }

    private bool IsPlayerLookingAtTV()
    {
        RaycastHit hit;
        Vector3 directionToTV = (transform.position - player.position).normalized;
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
        if (showInteractionMessage)
        {
            // The position and size of the label.
            Rect labelRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);

            string message = "Press (E) to monitor cameras";

            // Render label.
            GUI.Label(labelRect, message);
        }
    }
}
