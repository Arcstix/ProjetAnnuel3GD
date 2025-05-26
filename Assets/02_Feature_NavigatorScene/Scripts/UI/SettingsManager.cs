using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private PlayerCameraManager cameraManager;
    
    [SerializeField] private SettingSection controllerSection;
    [SerializeField] private SettingSection mouseSection;
    [SerializeField] private SettingSection musicSection;
    [SerializeField] private SettingSection sfxSection;

    public float controllerSpeed = 4;
    public float mouseSpeed = 4;

    public void SetCameraManager(PlayerCameraManager manager)
    {
        cameraManager = manager;
        SetAllSettings();
    }
    
    public void SetAllSettings()
    {
        if (cameraManager != null)
        {
            cameraManager.SetControllerSpeed(controllerSpeed);
            cameraManager.SetMouseSpeed(mouseSpeed);
            controllerSection.UpdateSectionValue(cameraManager.GetControllerSpeed());
            mouseSection.UpdateSectionValue(cameraManager.GetMouseSpeed());
        }
        else
        {
            controllerSection.UpdateSectionValue(controllerSpeed);
            mouseSection.UpdateSectionValue(mouseSpeed);
        }
    }
    
    public void UpdateControllerValue(float value)
    {
        if (cameraManager)
        {
            cameraManager.SetControllerSpeed(value);
        }
        controllerSpeed = value;
        controllerSection.UpdateSectionValue(controllerSpeed);
    }

    public void UpdateMouseValue(float value)
    {
        if (cameraManager)
        {
            cameraManager.SetMouseSpeed(value);
        }
        mouseSpeed = value;
        mouseSection.UpdateSectionValue(mouseSpeed);
    }

    public void UpdateMusicValue(float value)
    {
        
    }

    public void UpdateSfxValue(float value)
    {
        
    }
}
