using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovementState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        speedModifier = 1f;
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

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        movementStateMachine.ChangeState(movementStateMachine.SlowState);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
    }
}
