using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SoftLandingState : LandingState
{
    public event Action OnSoftLanding; 
    
    public SoftLandingState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnSoftLanding?.Invoke();
        CheckChargeRemaining();
    }

    public override void Tick()
    {
        base.Tick();
        
        timer += Time.deltaTime;

        if (timer >= 0.2f)
        {
            if (reusableData.OnLandingPlatform)
            {
                lastLandingPoint = targetLandingPoint;
                stateMachine.ChangeState(stateMachine.OnPlatformState);
            }
            else
            {
                lastLandingPoint = null;
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        reusableData.NumberOfConsecutiveTransportation = 0;
    }
}
