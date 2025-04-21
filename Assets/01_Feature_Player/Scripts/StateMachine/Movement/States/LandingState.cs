using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class LandingState : AirState
{
    public event Action OnLanding; 
    
    public LandingState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnLanding?.Invoke();
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
}
