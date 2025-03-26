using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerGroundedState
{
    private float timer;
    private JumpData jumpData;

    private float timeToApex;
    private float initialJumpVelocity;
    private float currentGravity;
    private float gravity;
    
    public event Action OnJump;
    
    public PlayerJumpState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        OnJump?.Invoke();
        jumpData = stateMachine.MovementManager.Metrics.CurrentMetrics.JumpData;
        reusableData.MovementSpeedModifier = jumpData.SpeedModifier;
        reusableData.hadJump = true;

        timeToApex = jumpData.JumpTimer / 2;
        gravity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpData.JumpHeight);
        currentGravity = gravity;
        initialJumpVelocity = (2 * jumpData.JumpHeight) / timeToApex;
        stateMachine.MovementManager.Rb.velocity = new Vector3(rigidbody.velocity.x, initialJumpVelocity, rigidbody.velocity.z);
        
        timer = 0;
    }

    public override void Tick()
    {
        base.Tick();
        
        timer += Time.deltaTime;
        
        if (timer >= jumpData.JumpTimer)
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        currentGravity += gravity * Time.deltaTime;
        
        stateMachine.MovementManager.Rb.velocity = new Vector3(rigidbody.velocity.x, currentGravity, rigidbody.velocity.z);
    }
}
