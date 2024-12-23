using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    private PlayerMovementManager movementManager;
    private PlayerAbilityManager abilityManager;
    private PlayerMetricsManager metricsManager;
    private PlayerCameraManager cameraManager;

    private void Awake()
    {
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
        cameraManager = GetComponent<PlayerCameraManager>();
    }

    private void Start()
    {
        InitMetrics();
        InitCamera();
        InitMovement();
        InitAbility();
    }

    private void InitCamera()
    {
        cameraManager.Init();
    }

    public void InitMovement()
    {
        movementManager.Init();
    }

    public void InitAbility()
    {
        abilityManager.Init();
    }

    public void InitInput()
    {

    }

    public void InitMetrics()
    {
        metricsManager.Init();
    }
}
