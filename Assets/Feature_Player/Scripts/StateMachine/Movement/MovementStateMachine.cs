using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine
{
    public PlayerMovementManager MovementManager { get; }

    public PlayerReusableStateData ReusableData { get; }

    public PlayerIdleState IdleState { get; }

    public RunningState RunningState { get; }

    public WalkState WalkState { get; }
    
    public JumpState JumpState { get; }

    public FallingState FallingState { get; }

    public LandingState LandingState { get; }
    public OnPlatformState OnPlatformState { get; }
    
    public WallRunState WallRunState { get; }
    
    public WallJumpState WallJumpState { get; }
    
    public TransportedState TransportedState { get; }

    public MovementStateMachine(PlayerMovementManager playerStateMachineManager)
    {
        MovementManager = playerStateMachineManager;
        ReusableData = playerStateMachineManager.ReusableData;

        IdleState = new PlayerIdleState(this);
        RunningState = new RunningState(this);
        WalkState = new WalkState(this);
        JumpState = new JumpState(this);
        FallingState = new FallingState(this);
        LandingState = new LandingState(this);
        OnPlatformState = new OnPlatformState(this);
        WallRunState = new WallRunState(this);
        WallJumpState = new WallJumpState(this);
        TransportedState = new TransportedState(this);
    }
}
