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
    private PlayerTargetSystem targetSystem;

    private void Awake()
    {
        reusableStateData = new PlayerReusableStateData();
        
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
        targetSystem = GetComponent<PlayerTargetSystem>();
        cameraManager = GetComponent<PlayerCameraManager>();
    }

    private void Start()
    {
        metricsManager.Init(reusableStateData);
        abilityManager.Init(reusableStateData);
        movementManager.Init(reusableStateData);
        targetSystem.Init(reusableStateData);
        cameraManager.Init(reusableStateData);
    }
}
