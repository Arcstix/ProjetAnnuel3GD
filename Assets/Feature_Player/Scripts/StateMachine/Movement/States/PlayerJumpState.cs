using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerGroundedState
{
    private float timer;
    private JumpData jumpData;
    
    public PlayerJumpState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        
        jumpData = stateMachine.MovementManager.Metrics.CurrentMetrics.JumpData;
        reusableData.MovementSpeedModifier = jumpData.SpeedModifier;
        reusableData.hadJump = true;
        
        float jumpForce = Mathf.Sqrt(-2f * Physics.gravity.y * jumpData.JumpHeight);
        stateMachine.MovementManager.Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

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
}
