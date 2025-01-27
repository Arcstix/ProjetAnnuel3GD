using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    private PlayerReusableStateData reusableStateData;
    
    private PlayerMovementManager movementManager;
    private PlayerAbilityManager abilityManager;
    private PlayerMetricsManager metricsManager;
    private PlayerCameraManager cameraManager;
    private PlayerSoundManager soundManager;

    private void Awake()
    {
        reusableStateData = new PlayerReusableStateData();
        
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
        cameraManager = GetComponent<PlayerCameraManager>();
        soundManager = GetComponent<PlayerSoundManager>();
    }

    private void Start()
    {
        metricsManager.Init(reusableStateData);
        abilityManager.Init(reusableStateData);
        movementManager.Init(reusableStateData);
        cameraManager.Init(reusableStateData);
        soundManager.Init(reusableStateData);
    }
}
