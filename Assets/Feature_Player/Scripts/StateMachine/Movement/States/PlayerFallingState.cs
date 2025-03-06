using System;
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
        timer = 0;
        reusableData.InAir = true;
        if (reusableData.hadJump)
        {
            reusableData.ShouldSlowDown = false;
        }
        else
        {
            reusableData.ShouldSlowDown = true;
        }
        reusableData.MovementSpeedModifier = metricsManager.CurrentMetrics.FallingData.SpeedModifier;
    }

    public override void Exit()
    {
        ResetSlowDown();
        base.Exit();
        reusableData.hadJump = false;
    }

    public override void Tick()
    {
        base.Tick();
        
        HandleRotation(GetMovementDirection());
        
        timer += Time.deltaTime;
        GroundedData groundedData = metricsManager.CurrentMetrics.GroundedData;
        
        if (timer <= groundedData.GravityModifier.keys[groundedData.GravityModifier.length - 1].time && reusableData.ShouldSlowDown)
        {
            SlowDown();
        }
        else
        {
            if (reusableData.ShouldSlowDown)
            {
                reusableData.ShouldSlowDown = false;
            }
            rigidbody.AddForce(Physics.gravity * groundedData.GravityMultiplier - GetCurrentVerticalVelocity(), ForceMode.Force);
        }

        if (reusableData.OnTransportation)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }

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

