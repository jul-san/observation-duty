using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics;

public class TerminalController : MonoBehaviour
{
    public TMP_InputField inputField;
    public KeyCode toggleKey = KeyCode.BackQuote; // The "~" key
    public Timer timer;
    public GameObject crateObject;
    public GameObject barrelObject;
    public GameObject trashCanObject;
    public GameObject palletObject;
    public GameObject cartObject;
    public int requiredCorrectScans = 3; // Number of correct scans required to win
    private int correctScans = 0; // Counter for correct scans


    private bool isTerminalActive = false;
    private Dictionary<string, Vector3> initialPositions = new Dictionary<string, Vector3>();

    private void Start()
    {
        // Log the initial positions of the objects
        LogInitialPosition("Crate", crateObject);
        LogInitialPosition("Barrel", barrelObject);
        LogInitialPosition("TrashCan", trashCanObject);
        LogInitialPosition("Pallet", palletObject);
        LogInitialPosition("Cart", cartObject);
    }

    private void LogInitialPosition(string objectName, GameObject obj)
    {
        if (obj != null)
        {
            initialPositions[objectName] = obj.transform.position;
            UnityEngine.Debug.Log($"{objectName} initial position: {obj.transform.position}");
        }
        else
        {
            UnityEngine.Debug.LogWarning($"{objectName} object reference is missing!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTerminal();
        }
    }

    private void ToggleTerminal()
    {
        isTerminalActive = !isTerminalActive;
        inputField.gameObject.SetActive(isTerminalActive);

        if (isTerminalActive)
        {
            inputField.ActivateInputField();
            inputField.onEndEdit.AddListener(ProcessCommand);
        }
        else
        {
            inputField.onEndEdit.RemoveListener(ProcessCommand);
            inputField.DeactivateInputField();
        }
    }

    private void ProcessCommand(string command)
    {
        // Clear the input field
        inputField.text = string.Empty;

        // Convert the command to lowercase for case-insensitive comparison
        command = command.ToLower();

        if (command.StartsWith("scan "))
        {
            string objectName = command.Substring(5).Trim(); // Extract the object name from the command and trim leading/trailing spaces

            GameObject obj = GetObjectByKeyword(objectName);

            if (obj != null)
            {
                string objectKey = GetObjectKey(obj);

                if (obj.transform.position != initialPositions[objectKey])
                {
                    obj.transform.position = initialPositions[objectKey];
                    UnityEngine.Debug.Log($"{objectKey} position reset to initial position.");
                    correctScans++; // Increment the correct scans counter
                    CheckWinCondition(); // Check if the win condition is met
                }
                else
                {
                    UnityEngine.Debug.Log($"{objectKey} is already at its initial position.");
                    timer.timeRemaining -= 10; // Deduct 10 seconds from the timer
                }
            }
            else
            {
                UnityEngine.Debug.Log($"Unknown object: {objectName}");
                timer.timeRemaining -= 10; // Deduct 10 seconds from the timer
            }
        }
        else
        {
            UnityEngine.Debug.Log($"Invalid command: {command}");
            timer.timeRemaining -= 10; // Deduct 10 seconds from the timer
        }

        // Deactivate the input field after processing the command
        inputField.DeactivateInputField();
    }

    private void CheckWinCondition()
    {
        if (correctScans >= requiredCorrectScans)
        {
            UnityEngine.Debug.Log("Congratulations! You have scanned all objects correctly and won the game!");
            // Add your game win logic here, such as displaying a win message, transitioning to a new scene, etc.
        }
    }

    private GameObject GetObjectByKeyword(string objectName)
    {
        if (objectName.Contains("crate"))
        {
            return crateObject;
        }
        else if (objectName.Contains("barrel"))
        {
            return barrelObject;
        }
        else if (objectName.Contains("trash can"))
        {
            return trashCanObject;
        }
        else if (objectName.Contains("cart"))
        {
            return cartObject;
        }
        else if (objectName.Contains("pallet"))
        {
            return palletObject;
        }
        else
        {
            return null;
        }
    }

    private string GetObjectKey(GameObject obj)
    {
        if (obj == crateObject)
        {
            return "Crate";
        }
        else if (obj == barrelObject)
        {
            return "Barrel";
        }
        else if (obj == trashCanObject)
        {
            return "TrashCan";
        }
        else if (obj == cartObject)
        {
            return "Cart";
        }
        else if (obj == palletObject)
        {
            return "Pallet";
        }
        else
        {
            return string.Empty;
        }
    }
}