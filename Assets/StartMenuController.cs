using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenu; 
    public GameObject playerCamera;
    public GameObject textCanvas;

    void Start()
    {
        // Show the cursor when the game starts
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Ensure the start menu is active and the player camera is inactive at the start
        textCanvas.SetActive(true);
        startMenu.SetActive(true);
        playerCamera.SetActive(false);
    }

    public void StartGame()
    {
        // Disable the start menu
        startMenu.SetActive(false);

        // Enable the player camera
        playerCamera.SetActive(true);
        
        textCanvas.SetActive(false);

        // Hide and lock the cursor when the game starts
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
