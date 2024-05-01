using UnityEngine;
using TMPro;

public class CameraTextChanger : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] cameraNames = { "hallway", "smallStorage", "underCatwalk", "upperCatwalk", "warehouseOverheadOne", "warehouseOverheadTwo" };

    private int currentCameraIndex = 0;
    private bool textEnabled = false;

    private void Update()
    {
        // Toggle text visibility when the comma key is pressed
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            textEnabled = !textEnabled;
            UpdateTextVisibility();
        }

        // Move to the next camera when the right arrow key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameraNames.Length;
            UpdateTextContent();
        }

        // Move to the previous camera when the left arrow key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCameraIndex = (currentCameraIndex - 1 + cameraNames.Length) % cameraNames.Length;
            UpdateTextContent();
        }
    }

    private void UpdateTextVisibility()
    {
        textComponent.gameObject.SetActive(textEnabled);
    }

    private void UpdateTextContent()
    {
        if (textEnabled)
        {
            textComponent.text = cameraNames[currentCameraIndex];
        }
    }
}