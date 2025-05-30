using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private ParticleSystem GroundFall;
    [SerializeField] private ParticleSystem Dash;
    
    private PlayerMovementManager movementManager;
    
    private PlayerReusableStateData reusableData;

    private void Awake()
    {
        movementManager = GetComponent<PlayerMovementManager>();

        movementManager.OnMovementStarted += GetReusableData;
    }

    private void GetReusableData()
    {
        reusableData = movementManager.ReusableData;
    }

    private void Update()
    {
        if (speedEffect == null) return;

        if (reusableData != null)
        {
            if (reusableData.OnTransportation || reusableData.IsWallRunning)
            {
                speedEffect.Play();
            }
            else
            {
                speedEffect.Stop();
            }

            if (reusableData.OnLandingPlatform)
            {
                GroundFall.Play();
            }
            else
            {
                GroundFall.Stop();
            }
            
        }
    }
}
