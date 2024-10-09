using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlowState : PlayerGroundedState
{
    public PlayerSlowState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movementStateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        movementStateMachine.ChangeState(movementStateMachine.RunningState);
    }
}
