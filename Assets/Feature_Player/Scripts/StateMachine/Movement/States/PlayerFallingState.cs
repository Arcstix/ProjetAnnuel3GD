using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState
{
    public PlayerFallingState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        reusableData.MovementSpeedModifier = metricsManager.CurrentPlayerSO.GroundedData.RunData.SpeedModifier;
    }

    public override void Exit()
    {
        ResetSlowDown();
        base.Exit();
    }

    public override void Tick()
    {
        HandleRotation(GetMovementDirection());

        // if (reusableData.ShouldSlowDown)
        // {
        //     SlowDown();
        //     timer += Time.deltaTime;
        //     if (timer >= 2)
        //     {
        //         ResetSlowDown();
        //     }
        // }
        // else
        // {
        // }
        rigidbody.AddForce(Physics.gravity * groundedData.GravityMultiplier - GetCurrentVerticalVelocity(), ForceMode.Acceleration);

        if(!stateMachine.ReusableData.InAir)
        {
            stateMachine.ChangeState(stateMachine.LandingState);
        }
    }

    /// <summary>
    /// Reset le timer et le bool�en SlowDown � false.
    /// </summary>
    private void ResetSlowDown()
    {
        stateMachine.ReusableData.ShouldSlowDown = false;
        timer = 0;
    }
}

