using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    private PlayerMovementManager movementManager;
    private PlayerAbilityManager abilityManager;
    private PlayerMetricsManager metricsManager;

    private void Awake()
    {
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
    }

    private void Start()
    {
        InitMetrics();
        InitMovement();
        InitAbility();
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
