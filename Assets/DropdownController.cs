using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public Dropdown dropdown;
    public KeyCode toggleKey = KeyCode.G;

    private bool isDropdownActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleDropdown();
        }

        if (isDropdownActive)
        {
            NavigateDropdown();
        }
    }

    private void ToggleDropdown()
    {
        isDropdownActive = !isDropdownActive;
        dropdown.gameObject.SetActive(isDropdownActive);

        if (isDropdownActive)
        {
            dropdown.value = dropdown.value; // Refresh the dropdown to update the selected option
        }
    }

    private void NavigateDropdown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int currentValue = dropdown.value;
            currentValue--;
            if (currentValue < 0)
            {
                currentValue = dropdown.options.Count - 1;
            }
            dropdown.value = currentValue;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            int currentValue = dropdown.value;
            currentValue++;
            if (currentValue >= dropdown.options.Count)
            {
                currentValue = 0;
            }
            dropdown.value = currentValue;
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Handle the selection based on the dropdown value
            // You can add your own logic here
            UnityEngine.Debug.Log("Selected option: " + dropdown.options[dropdown.value].text);

            ToggleDropdown();
        }
    }
}