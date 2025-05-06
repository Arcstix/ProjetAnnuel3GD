using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirState
{
    private float timeToApex;
    private float initialJumpVelocity;
    private float currentGravity;
    private float gravity;
    private Vector3 startPosition;
    private Vector3 endPosition;
    
    public event Action OnJump;
    
    public JumpState(MovementStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        OnJump?.Invoke();
        jumpData = stateMachine.MovementManager.Metrics.CurrentMetrics.JumpData;
        reusableData.MovementSpeedModifier = jumpData.SpeedModifier;
        reusableData.HadJump = true;
        // startPosition = stateMachine.MovementManager.transform.position;
        // endPosition = new Vector3(startPosition.x, startPosition.y + jumpData.JumpHeight, startPosition.z);
        
        gravity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpData.JumpHeight);
        currentGravity = gravity;
        initialJumpVelocity = (2 * jumpData.JumpHeight) / jumpData.JumpTimer;
        stateMachine.MovementManager.Rb.velocity = new Vector3(rigidbody.velocity.x, initialJumpVelocity, rigidbody.velocity.z);
        
        timer = 0;
    }

    public override void Tick()
    {
        base.Tick();
        
        timer += Time.deltaTime;
        DetectLandingZone();
        
        if (timer >= jumpData.JumpTimer)
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        currentGravity += gravity * Time.fixedDeltaTime;
        
        stateMachine.MovementManager.Rb.velocity = new Vector3(rigidbody.velocity.x, currentGravity, rigidbody.velocity.z);
    }
}
