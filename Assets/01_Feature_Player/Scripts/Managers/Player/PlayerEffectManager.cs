using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private ParticleSystem GroundFall;
    
    private PlayerMovementManager movementManager;
    
    private PlayerReusableStateData reusableData;
    private GroundCheck groundCheck;

    private void Awake()
    {
        movementManager = GetComponent<PlayerMovementManager>();
        movementManager.OnMovementStarted += GetReusableData;
        groundCheck = GetComponent<GroundCheck>();
    }

    private void GetReusableData()
    {
        reusableData = movementManager.ReusableData;
        movementManager.StateMachine.HardLandingState.OnHardLanding += PlayGroundFall;
    }

    private void PlayGroundFall()
    {
        if(groundCheck.DetectSurface()!= GroundTypeEnum.None)
        { 
            GroundFall.Play();  
        }
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
            
        }
    }
}
