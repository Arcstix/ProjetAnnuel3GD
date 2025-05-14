using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMetricsManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private PlayerSO metricsPlayer;

    public bool useCharge = true;

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
    private float currentChargeRight;
    private float currentChargeLeft;

    private Rigidbody playerRb;
    private PlayerSO currentMetrics;
    private PlayerReusableStateData reusableData;
    
    private float currentExternForce = 1;
    
    public event Action OnMetricsSet;
    public event Action<float, float> OnRightStaminaSet; // param : current Stamina, max Stamina
    public event Action<float, float> OnLeftStaminaSet;
    
    public event Action<float, float> OnRightChargeSet;
    public event Action<float, float> OnLeftChargeSet;

    public event Action OnRightChargeReady;
    public event Action OnLeftChargeReady;
    public event Action OnNoCharge;
    
    public void Init(PlayerReusableStateData reusableStateData)
    {
        //InitializeDictionary();
        playerRb = GetComponent<Rigidbody>();
        currentMetrics = metricsPlayer;
        SetRigidbodyMetrics();
        reusableData = reusableStateData;
        OnMetricsSet?.Invoke();
        if (useCharge)
        {
            UpdateLeftCharge(currentMetrics.StaminaData.MaxStamina, true);
            UpdateRightCharge(currentMetrics.StaminaData.MaxStamina, true);
        }
        else
        {
            UpdateLeftStamina(currentMetrics.StaminaData.MaxStamina);
            UpdateRightStamina(currentMetrics.StaminaData.MaxStamina);
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (currentExternForce > 1)
        {
            currentExternForce = Mathf.Lerp(currentExternForce, 1, Time.deltaTime);
        }

        if (reusableData is { InAir: false, OnTransportation: false })
        {
            if (useCharge)
            {
                RecoverCharge();
            }
            else
            {
                RecoverStamina();
            }
        }

        if (reusableData is { OnTransportation: true })
        {
            if (!useCharge)
            {
                ConsumeStamina();
            }
        }
    }

    private void ConsumeStamina()
    {
        if (reusableData.LeftActivation)
        {
            if (reusableData.LeftParent == null)
            {
                UpdateLeftStamina(Mathf.Max(0, staminaLeft - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate));
            }
        }

        if (reusableData.RightActivation)
        {
            if (reusableData.RightParent == null)
            {
                UpdateRightStamina(Mathf.Max(0, staminaRight - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate));
            }
        }
    }

    public void ConsumeCharge(bool rightStamina)
    {
        if (rightStamina)
        {
            if (reusableData.RightParent == null)
            {
                float numberOfCharge = (float)metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
                UpdateRightCharge(currentChargeRight - metricsPlayer.StaminaData.MaxStamina / numberOfCharge);
            }
        }
        else
        {
            if (reusableData.LeftParent == null)
            {
                float numberOfCharge = (float)metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
                UpdateLeftCharge(currentChargeLeft - metricsPlayer.StaminaData.MaxStamina / numberOfCharge);
            }
        }
    }
    
    private void UpdateRightCharge(float amount, bool init = false)
    {
        currentChargeRight = amount;
        int staminaForACharge = metricsPlayer.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
        if (Mathf.Approximately(currentChargeRight, staminaForACharge))
        {
            if (!init)
            {
                OnRightChargeReady?.Invoke();
            }
        }
        OnRightChargeSet?.Invoke(currentChargeRight, metricsPlayer.StaminaData.MaxStamina);
    }

    private void UpdateLeftCharge(float amount, bool init = false)
    {
        currentChargeLeft = amount;
        int staminaForACharge = metricsPlayer.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
        if (Mathf.Approximately(currentChargeLeft, staminaForACharge))
        {
            if (!init)
            {
                OnLeftChargeReady?.Invoke();
            }
        }
        OnLeftChargeSet?.Invoke(currentChargeLeft, metricsPlayer.StaminaData.MaxStamina);
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
    
    private void RecoverCharge()
    {
        if (currentChargeLeft < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateLeftCharge(Mathf.Min(metricsPlayer.StaminaData.MaxStamina,
                currentChargeLeft + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate));
        }

        if (currentChargeRight < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateRightCharge(Mathf.Min(metricsPlayer.StaminaData.MaxStamina,
                currentChargeRight + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate));
        }
    }

    public void RecoverFullCharge()
    {
        UpdateLeftCharge(currentMetrics.StaminaData.MaxStamina);
        UpdateRightCharge(currentMetrics.StaminaData.MaxStamina);
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

    public bool HasLeftCharge()
    {
        float numberOfCharge = (float)metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
        if (currentChargeLeft >= currentMetrics.StaminaData.MaxStamina / numberOfCharge)
        {
            return true;
        }
        
        OnNoCharge?.Invoke();
        return false;
    }
    
    public bool HasRightCharge()
    {
        float numberOfCharge = (float)metricsPlayer.StaminaData.MaxNumberOfChargePerObject;
        if (currentChargeRight >= currentMetrics.StaminaData.MaxStamina / numberOfCharge)
        {
            return true;
        }
        
        OnNoCharge?.Invoke();
        return false;
    }
}
