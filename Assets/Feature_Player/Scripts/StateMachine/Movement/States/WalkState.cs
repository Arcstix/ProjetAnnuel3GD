using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : GroundedState
{
    public event Action OnWalking;
    
    public WalkState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnWalking?.Invoke();
        reusableData.MovementSpeedModifier = metricsManager.CurrentMetrics.GroundedData.WalkData.SpeedModifier;
    }

    public override void Tick()
    {
        base.Tick();
        
        if ((!reusableData.OnTransportation && !reusableData.CanMove) || reusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        stateMachine.ChangeState(stateMachine.RunningState);
    }
}
