using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState
{
    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Falling");
        Debug.Log("SlowDown : " + reusableData.ShouldSlowDown);
        reusableData.MovementSpeedModifier = metricsManager.CurrentPlayerSO.GroundedData.RunData.SpeedModifier;
    }

    public override void Exit()
    {
        ResetSlowDown();
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        HandleRotation(GetMovementDirection());

        if (reusableData.ShouldSlowDown)
        {
            SlowDown();
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                Debug.Log("Timer écoulé");
                ResetSlowDown();
            }
        }
        else
        {
            rigidbody.AddForce(Physics.gravity * groundedData.GravityMultiplier, ForceMode.Acceleration);
        }

        if(!movementStateMachine.ReusableData.InAir)
        {
            movementStateMachine.ChangeState(movementStateMachine.LandingState);
        }
    }

    /// <summary>
    /// Reset le timer et le booléen SlowDown à false.
    /// </summary>
    private void ResetSlowDown()
    {
        movementStateMachine.ReusableData.ShouldSlowDown = false;
        timer = 0;
    }
}

