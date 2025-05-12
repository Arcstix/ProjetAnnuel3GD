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
