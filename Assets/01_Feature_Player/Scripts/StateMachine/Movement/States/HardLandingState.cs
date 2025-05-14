using System;
using UnityEngine;

public class HardLandingState : LandingState
{
    public event Action OnHardLanding; 
    
    public HardLandingState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnHardLanding?.Invoke();
        CheckChargeRemaining();
        reusableData.NumberOfConsecutiveTransportation = 0;
        reusableData.NumberOfConsecutiveWallRun = 0;
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
