using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle State");
        movementStateMachine.ReusableData.MovementInput = Vector2.zero;
        movementStateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVelocity();
    }

    public override void Tick()
    {
        base.Tick();

        if(movementStateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        if (movementStateMachine.ReusableData.CanMove)
        {
            OnMove();
        }        
    }
}
