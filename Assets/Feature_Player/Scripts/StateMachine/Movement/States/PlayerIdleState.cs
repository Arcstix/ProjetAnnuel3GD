using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        reusableData.MovementInput = Vector2.zero;
        reusableData.MovementSpeedModifier = 0f;
    }

    public override void Tick()
    {
        HandleRotation(GetMovementDirection());
        
        if (!reusableData.InAir && !reusableData.OnTransportation)
        {
            ResetVelocity();
        }

        if (reusableData.InAir && !reusableData.OnTransportation)
        {
            // Change to Falling State
            stateMachine.ChangeState(stateMachine.FallingState);
            return;
        }

        if (reusableData.CanMove && !reusableData.InAir && !reusableData.OnTransportation && reusableData.MovementInput != Vector2.zero)
        {
            // Change to Running State
            OnMove();
            return;
        }

    }
}
