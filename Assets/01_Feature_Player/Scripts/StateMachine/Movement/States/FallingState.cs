using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : AirState
{
    private float verticalVelocity;

    public event Action OnFalling;
    
    public FallingState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnFalling?.Invoke();
        timer = 0;
        verticalVelocity = Physics.gravity.y * fallingData.GravityMultiplier;
        if (!reusableData.HadJump)
        {
            stateMachine.MovementManager.Rb.velocity = new Vector3(stateMachine.MovementManager.Rb.velocity.x, 0, stateMachine.MovementManager.Rb.velocity.z);
        }
        reusableData.InAir = true;
        reusableData.ShouldSlowDown = !reusableData.HadJump;
        reusableData.MovementSpeedModifier = fallingData.SpeedModifier;
    }

    public override void Exit()
    {
        ResetSlowDown();
        base.Exit();
        reusableData.HadJump = false;
        timer = 0;
    }

    public override void Tick()
    {
        base.Tick();
        
        HandleRotation(GetMovementDirection());
        
        timer += Time.deltaTime;
        
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (Mathf.Abs(verticalVelocity) < fallingData.MaxFallingSpeed)
        {
            verticalVelocity += Physics.gravity.y * fallingData.GravityMultiplier * Time.deltaTime;
        }
        
        if (timer <= fallingData.GravityModifier.keys[fallingData.GravityModifier.length - 1].time && reusableData.ShouldSlowDown)
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

        if (jump.WasPressedThisFrame() && reusableData.InAir && reusableData.NumberOfJump < jumpData.MaxAirJumps)
        {
            reusableData.NumberOfJump++;
            stateMachine.ChangeState(stateMachine.JumpState);
        }

        if(!stateMachine.ReusableData.InAir || reusableData.OnLandingPlatform)
        {
            reusableData.NumberOfJump = 0;

            if (Mathf.Approximately(Mathf.Abs(verticalVelocity), fallingData.MaxFallingSpeed))
            {
                stateMachine.ChangeState(stateMachine.HardLandingState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.SoftLandingState);
            }
        }
    }
    
    public void SlowDown()
    {
        //ajouter une force pour ralentir le joueur avec une animation curve
        rigidbody.AddForce(Physics.gravity * (fallingData.GravityMultiplier * fallingData.GravityModifier.Evaluate(timer)) - GetCurrentVerticalVelocity(), ForceMode.Acceleration);
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

