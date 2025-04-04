using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState
{
    private float verticalVelocity;

    public event Action OnFalling;
    
    public PlayerFallingState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnFalling?.Invoke();
        timer = 0;
        verticalVelocity = Physics.gravity.y * metricsManager.CurrentMetrics.FallingData.GravityModifier;
        reusableData.InAir = true;
        reusableData.ShouldSlowDown = !reusableData.HadJump;
        reusableData.MovementSpeedModifier = metricsManager.CurrentMetrics.FallingData.SpeedModifier;
    }

    public override void Exit()
    {
        ResetSlowDown();
        base.Exit();
        reusableData.HadJump = false;
    }

    public override void Tick()
    {
        base.Tick();
        
        HandleRotation(GetMovementDirection());
        
        timer += Time.deltaTime;
        
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (verticalVelocity < metricsManager.CurrentMetrics.FallingData.MaxFallingSpeed)
        {
            verticalVelocity += Physics.gravity.y * metricsManager.CurrentMetrics.FallingData.GravityModifier * Time.deltaTime;
        }
        
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
            rigidbody.AddForce(new Vector3(0, verticalVelocity, 0) - GetCurrentVerticalVelocity(), ForceMode.Force);
        }

        if (reusableData.OnTransportation)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }

        if (input.PlayerActions.Jump.WasPressedThisFrame() && reusableData.InAir && reusableData.NumberOfJump < metricsManager.CurrentMetrics.JumpData.MaxAirJumps)
        {
            reusableData.NumberOfJump++;
            stateMachine.ChangeState(stateMachine.JumpState);
        }

        if(!stateMachine.ReusableData.InAir)
        {
            reusableData.NumberOfJump = 0;
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

