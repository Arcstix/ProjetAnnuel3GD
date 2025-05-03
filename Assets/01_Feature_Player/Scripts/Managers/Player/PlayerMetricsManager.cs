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
    public int StaminaRight
    {
        get => staminaRight;
        set => staminaRight = value;
    }
    public int StaminaLeft
    {
        get => staminaLeft;
        set => staminaLeft = value;
    }
    
    private int staminaRight = 0;
    private int staminaLeft = 0;
    private int currentChargeRight;
    private int currentChargeLeft;

    private Rigidbody playerRb;
    private PlayerSO currentMetrics;
    private PlayerReusableStateData reusableData;
    
    private float currentExternForce = 1;
    
    public event Action OnMetricsSet;
    public event Action<float, float> OnRightStaminaSet; // param : current Stamina, max Stamina
    public event Action<float, float> OnLeftStaminaSet;
    
    public event Action<float, float> OnRightChargeSet;
    public event Action<float, float> OnLeftChargeSet;
    
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
            UpdateLeftCharge(currentMetrics.StaminaData.MaxStamina);
            UpdateRightCharge(currentMetrics.StaminaData.MaxStamina);
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
                UpdateLeftStamina(Mathf.Max(0, Mathf.RoundToInt(staminaLeft - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate)));
            }
        }

        if (reusableData.RightActivation)
        {
            if (reusableData.RightParent == null)
            {
                UpdateRightStamina(Mathf.Max(0, Mathf.RoundToInt(staminaRight - Time.deltaTime * currentMetrics.StaminaData.ConsumptionRate)));
            }
        }
    }

    public void ConsumeCharge(bool rightStamina)
    {
        if (rightStamina)
        {
            if (reusableData.RightParent == null)
            {
                UpdateRightCharge(currentChargeRight - metricsPlayer.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject);
            }
        }
        else
        {
            if (reusableData.LeftParent == null)
            {
                UpdateLeftCharge(currentChargeLeft - metricsPlayer.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject);
            }
        }
    }
    
    private void UpdateRightCharge(int amount)
    {
        currentChargeRight = amount;
        OnRightChargeSet?.Invoke(currentChargeRight, metricsPlayer.StaminaData.MaxStamina);
    }

    private void UpdateLeftCharge(int amount)
    {
        currentChargeLeft = amount;
        OnLeftChargeSet?.Invoke(currentChargeLeft, metricsPlayer.StaminaData.MaxStamina);
    }

    private void RecoverStamina()
    {
        if (staminaLeft < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateLeftStamina(Mathf.Min(metricsPlayer.StaminaData.MaxStamina, Mathf.RoundToInt(staminaLeft + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate)));
        }

        if (staminaRight < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateRightStamina(Mathf.Min(metricsPlayer.StaminaData.MaxStamina, Mathf.RoundToInt(staminaRight + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate)));
        }
    }
    
    private void RecoverCharge()
    {
        if (currentChargeLeft < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateLeftCharge(Mathf.Min(metricsPlayer.StaminaData.MaxStamina,
                Mathf.RoundToInt(currentChargeLeft + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate)));
        }

        if (currentChargeRight < currentMetrics.StaminaData.MaxStamina)
        {
            UpdateRightCharge(Mathf.Min(metricsPlayer.StaminaData.MaxStamina,
                Mathf.RoundToInt(currentChargeRight + Time.deltaTime * currentMetrics.StaminaData.RecoveryRate)));
        }
    }

    private void UpdateRightStamina(int stamina)
    {
        staminaRight = stamina;
        OnRightStaminaSet?.Invoke(staminaRight, currentMetrics.StaminaData.MaxStamina);
    }

    private void UpdateLeftStamina(int stamina)
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
        if (currentChargeLeft >= currentMetrics.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject)
        {
            return true;
        }
        
        return false;
    }
    
    public bool HasRightCharge()
    {
        if (currentChargeRight >= currentMetrics.StaminaData.MaxStamina / metricsPlayer.StaminaData.MaxNumberOfChargePerObject)
        {
            return true;
        }
        
        return false;
    }
}
