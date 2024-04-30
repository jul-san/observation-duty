using UnityEngine;
using TMPro;

public class TerminalController : MonoBehaviour
{
    public TMP_InputField inputField;
    public KeyCode toggleKey = KeyCode.BackQuote; // The "~" key

    private bool isTerminalActive = false;

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
            inputField.onEndEdit.AddListener(ClearInputField);
        }
        else
        {
            inputField.onEndEdit.RemoveListener(ClearInputField);
            inputField.DeactivateInputField();
        }
    }

    private void ClearInputField(string command)
    {
        // Clear the input field
        inputField.text = string.Empty;

        // Deactivate the input field after clearing the text
        inputField.DeactivateInputField();
    }
}