using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public Camera startMenuCamera;
    public Camera playerCamera;

    private void Start()
    {
        // Ensure the start menu camera is active and the player camera is inactive
        startMenuCamera.enabled = true;
        playerCamera.enabled = false;
    }

    private void Update()
    {
        // Show the mouse cursor when the start menu canvas is enabled
        if (gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnNewGameClicked()
    {
        // Switch to the player camera
        startMenuCamera.enabled = false;
        playerCamera.enabled = true;

        // Hide the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Disable the start menu canvas
        gameObject.SetActive(false);
    }

    public void OnContinueClicked()
    {
        // Implement the logic for continuing the game, if applicable
    }

    public void OnOptionsClicked()
    {
        // Implement the logic for opening the options menu, if applicable
    }

    public void OnQuitClicked()
    {
        // Quit the game
        Application.Quit();
    }
}