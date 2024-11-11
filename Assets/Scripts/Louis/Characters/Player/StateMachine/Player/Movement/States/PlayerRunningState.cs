using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Running State");
        reusableData.MovementSpeedModifier = metricsManager.CurrentPlayerSO.GroundedData.RunData.SpeedModifier;
    }

    protected override void OnSlowStarted(InputAction.CallbackContext context)
    {
        base.OnSlowStarted(context);

        movementStateMachine.ChangeState(movementStateMachine.SlowState);
    }
}
