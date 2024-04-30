using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FogController : MonoBehaviour
{
    public float batteryDrainRate = 100000f;
    public Image batteryProgressBar;

    private bool isFogDecreasing = false;
    private float startingFogDensity;
    private float batteryLevel = .01f;

    private void Start()
    {
        startingFogDensity = RenderSettings.fogDensity;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && batteryLevel > 0f)
        {
            isFogDecreasing = true;
            ReduceFogDensity();
            DrainBattery();
        }
        else
        {
            isFogDecreasing = false;
            RestoreFogDensity();
        }
    }

    private void ReduceFogDensity()
    {
        RenderSettings.fogDensity = 0f;
    }

    private void RestoreFogDensity()
    {
        RenderSettings.fogDensity = startingFogDensity;
    }

    private void DrainBattery()
    {
        batteryLevel = Mathf.Max(batteryLevel - batteryDrainRate * Time.deltaTime, 0f);
        UnityEngine.Debug.Log("Battery Level: " + batteryLevel);
        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        batteryProgressBar.fillAmount = batteryLevel * 100;
        UnityEngine.Debug.Log("Fill Amount: " + batteryProgressBar.fillAmount);
    }
}