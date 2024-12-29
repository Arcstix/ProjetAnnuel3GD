using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        reusableData.MovementSpeedModifier = metricsManager.CurrentPlayerSO.GroundedData.RunData.SpeedModifier;
    }

    public override void Tick()
    {
        if (reusableData.InAir && !reusableData.OnTransportation)
        {
            stateMachine.ChangeState(stateMachine.FallingState);
            return;
        }
        
        if (reusableData.OnTransportation || !reusableData.CanMove || reusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        stateMachine.ChangeState(stateMachine.SlowState);
    }
}
