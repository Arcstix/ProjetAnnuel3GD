using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetricsManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private PlayerSO defaultPlayerSO;

    public PlayerSO CurrentPlayerSO { get => currentPlayerSO; set => currentPlayerSO = value; }
    
    public float ExternForce => currentExternForce;

    public float staminaRight = 0;
    public float staminaLeft = 0;

    private Rigidbody playerRb;
    private PlayerSO currentPlayerSO;
    
    private float currentExternForce = 1;
    
    public event Action OnMetricsSet;
    
    public void Init(PlayerReusableStateData reusableStateData)
    {
        //InitializeDictionary();
        playerRb = GetComponent<Rigidbody>();
        currentPlayerSO = defaultPlayerSO;
        SetRigidbodyMetrics();
        OnMetricsSet?.Invoke();
    }

    private void Update()
    {
        if (currentExternForce > 1)
        {
            currentExternForce = Mathf.Lerp(currentExternForce, 1, Time.deltaTime);
        }
    }

    public void SetRigidbodyMetrics()
    {
        playerRb.drag = CurrentPlayerSO.Drag;
    }

    public void AddExternForce(float forceValue)
    {
        currentExternForce += forceValue;
    }
}
