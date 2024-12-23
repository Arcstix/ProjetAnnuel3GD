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
        reusableData.MovementInput = Vector2.zero;
        reusableData.MovementSpeedModifier = 0f;

        if (!reusableData.InAir)
        {
            ResetVelocity();
        }
    }

    public override void Tick()
    {
        base.Tick();

        if(reusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        if (reusableData.CanMove && !reusableData.InAir)
        {
            OnMove();
            return;
        }

        HandleRotation(GetMovementDirection());
    }
}
