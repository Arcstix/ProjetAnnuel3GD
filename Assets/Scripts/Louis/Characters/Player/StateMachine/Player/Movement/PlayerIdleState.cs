using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerMovementState
{
    public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        speedModifier = 0f;

        ResetVelocity();
    }

    public override void Tick()
    {
        base.Tick();

        if(movementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    private void OnMove()
    {
        if (shooldSlow)
        {
            movementStateMachine.ChangeState(movementStateMachine.SlowState);
            return;
        }

        movementStateMachine.ChangeState(movementStateMachine.RunningState);
    }
}
