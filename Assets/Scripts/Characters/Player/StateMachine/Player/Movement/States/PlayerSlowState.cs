using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlowState : PlayerMovementState
{
    public PlayerSlowState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        speedModifier = 0.225f;
    }

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();

        movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();

        movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        movementStateMachine.ChangeState(movementStateMachine.RunningState);
    }
}
