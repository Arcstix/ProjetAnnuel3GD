using System;
using UnityEngine;

public class PlayerTimeManager : MonoBehaviour
{
    private PlayerAbilityManager abilityManager;
    private PlayerMetricsManager metricsManager;
    
    private float defaultTime = 1f;
    private float targetTime = 1f;
    
    private float currentTime = 1f;
    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();

        abilityManager.OnAbilityStarted += SubscribeEvent;
    }

    private void Update()
    {
        if (currentTime < targetTime)
        {
            // TimeScale back to normal
            currentTime = Mathf.Lerp(currentTime, targetTime,
                metricsManager.CurrentPlayerSO.AbilityData.DefaultLerpTime);
            
            Time.timeScale = currentTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else if (currentTime > targetTime)
        {
            // Timescale go on slow mode
            currentTime = Mathf.Lerp(currentTime, targetTime,
                metricsManager.CurrentPlayerSO.AbilityData.SlowLerpTime);
            
            Time.timeScale = currentTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void OnDisable()
    {
        abilityManager.AbilityStateMachine.AimState.OnAim -= SlowTime;
        abilityManager.AbilityStateMachine.AimState.OnRelease -= DefaultTime;
    }

    private void SubscribeEvent()
    {
        abilityManager.AbilityStateMachine.AimState.OnAim += SlowTime;
        abilityManager.AbilityStateMachine.AimState.OnRelease += DefaultTime;
    }

    private void DefaultTime()
    {
        targetTime = defaultTime;
    }

    private void SlowTime()
    {
        if (metricsManager.CurrentPlayerSO != null)
        {
            if (abilityManager.ReusableData.InAir)
            {
                targetTime = metricsManager.CurrentPlayerSO.AbilityData.SlowTime;
            }
        }
    }
}
