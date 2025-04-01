using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerMovementManager MovementManager { get; }

    public PlayerReusableStateData ReusableData { get; }

    public PlayerIdleState IdleState { get; }

    public PlayerRunningState RunningState { get; }

    public PlayerWalkState WalkState { get; }
    
    public PlayerJumpState JumpState { get; }

    public PlayerFallingState FallingState { get; }

    public PlayerLandingState LandingState { get; }
    
    public PlayerWallRunState WallRunState { get; }
    
    public PlayerWallJumpState WallJumpState { get; }

    public PlayerMovementStateMachine(PlayerMovementManager playerStateMachineManager)
    {
        MovementManager = playerStateMachineManager;
        ReusableData = playerStateMachineManager.ReusableData;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        WalkState = new PlayerWalkState(this);
        JumpState = new PlayerJumpState(this);
        FallingState = new PlayerFallingState(this);
        LandingState = new PlayerLandingState(this);
        WallRunState = new PlayerWallRunState(this);
        WallJumpState = new PlayerWallJumpState(this);
    }
}
