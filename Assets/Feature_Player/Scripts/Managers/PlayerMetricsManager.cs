using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetricsManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private PlayerSO defaultPlayerSO;

    public PlayerSO CurrentPlayerSO { get => currentPlayerSO; set => currentPlayerSO = value; }

    private Dictionary<int, float> abilityDictionary = new Dictionary<int, float>();

    private Rigidbody playerRb;
    private PlayerSO currentPlayerSO;

    public void Init()
    {
        //InitializeDictionary();
        playerRb = GetComponent<Rigidbody>();
        currentPlayerSO = defaultPlayerSO;
        SetRigidbodyMetrics();
    }

    public void SetRigidbodyMetrics()
    {
        playerRb.drag = CurrentPlayerSO.Drag;
    }
}
