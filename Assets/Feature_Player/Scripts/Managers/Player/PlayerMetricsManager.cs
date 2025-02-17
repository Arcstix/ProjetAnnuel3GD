using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMetricsManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private PlayerSO metricsPlayer;

    public PlayerSO CurrentMetrics { get => currentMetrics; set => currentMetrics = value; }
    
    public float ExternForce => currentExternForce;
    public float StaminaRight
    {
        get => staminaRight;
        set => staminaRight = value;
    }
    public float StaminaLeft
    {
        get => staminaLeft;
        set => staminaLeft = value;
    }
    
    private float staminaRight = 0;
    private float staminaLeft = 0;

    private Rigidbody playerRb;
    private PlayerSO currentMetrics;
    private PlayerReusableStateData reusableData;
    
    private float currentExternForce = 1;
    
    public event Action OnMetricsSet;
    public event Action<float, float> OnRightStaminaSet; // param : current Stamina, max Stamina
    public event Action<float, float> OnLeftStaminaSet;
    
    public void Init(PlayerReusableStateData reusableStateData)
    {
        //InitializeDictionary();
        playerRb = GetComponent<Rigidbody>();
        currentMetrics = metricsPlayer;
        SetRigidbodyMetrics();
        reusableData = reusableStateData;
        OnMetricsSet?.Invoke();
        UpdateLeftStamina(currentMetrics.StaminaData.MaxStamina);
        UpdateRightStamina(currentMetrics.StaminaData.MaxStamina);
    }

    private void Update()
    {
        if (currentExternForce > 1)
        {
            currentExternForce = Mathf.Lerp(currentExternForce, 1, Time.deltaTime);
        }

        if (reusableData is { InAir: false, OnTransportation: false })
        {
            RecoverStamina();
        }

        if (reusableData is { OnTransportation: true })
        {
            Debug.Log("Consume");
            ConsumeStamina();
        }
    }

    private void ConsumeStamina()
    {
        if (reusableData.LeftActivation)
        {
            UpdateLeftStamina(Mathf.Max(0, staminaLeft - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate));
        }

        if (reusableData.RightActivation)
        {
            UpdateRightStamina(Mathf.Max(0, staminaRight - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate));
        }
    }

    private void RecoverStamina()
    {
        if (staminaLeft < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateLeftStamina(Mathf.Min(metricsPlayer.StaminaData.MaxStamina, staminaLeft + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate));
        }

        if (staminaRight < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateRightStamina(Mathf.Min(metricsPlayer.StaminaData.MaxStamina, staminaRight + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate));
        }
    }

    private void UpdateRightStamina(float stamina)
    {
        staminaRight = stamina;
        OnRightStaminaSet?.Invoke(staminaRight, currentMetrics.StaminaData.MaxStamina);
    }

    private void UpdateLeftStamina(float stamina)
    {
        staminaLeft = stamina;
        OnLeftStaminaSet?.Invoke(staminaLeft, currentMetrics.StaminaData.MaxStamina);
    }

    private void SetRigidbodyMetrics()
    {
        playerRb.drag = CurrentMetrics.Drag;
    }

    public void AddExternForce(float forceValue)
    {
        currentExternForce += forceValue;
    }
}
