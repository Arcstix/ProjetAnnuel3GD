using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunningState : GroundedState
{
    public event Action OnRunning;
    
    public RunningState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnRunning?.Invoke();
        reusableData.MovementSpeedModifier = metricsManager.CurrentMetrics.GroundedData.RunData.SpeedModifier;
    }

    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.InAir && !reusableData.OnTransportation)
        {
            stateMachine.ChangeState(stateMachine.FallingState);
            return;
        }
        
        if ((!reusableData.OnTransportation && !reusableData.CanMove) || reusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        stateMachine.ChangeState(stateMachine.WalkState);
    }
}
